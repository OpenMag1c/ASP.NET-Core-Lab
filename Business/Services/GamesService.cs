﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business.DTO;
using Business.ExceptionMiddleware;
using Business.Helper;
using Business.Interfaces;
using Business.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DAL.Enum;
using DAL.Interfaces;
using DAL.Models;

namespace Business.Services
{
    public class GamesService : IGamesService
    {
        private readonly IRepositoryBase<Product> _productRepo;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;

        public GamesService(IRepositoryBase<Product> productRepo, IMapper mapper, Cloudinary cloudinary)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _cloudinary = cloudinary;
        }

        public IEnumerable<ProductOutputDTO> GetProducts(Pagination pagination, ProductFilters filters)
        {
            var products = _productRepo.FindAll(false);
            products = FilterProducts(products, filters);
            var pagedList = PagedProducts<Product>.ToPagedList(products, pagination.PageNumber, pagination.PageSize);
            var result = pagedList.Select(prod => _mapper.Map<ProductOutputDTO>(prod));
            return result;
        }

        private IQueryable<Product> FilterProducts(IQueryable<Product> products, ProductFilters filters)
        {
            var genre = filters.FilterByGenre;
            var age = filters.FilterByAge;
            if (genre != Genres.AllGenres)
            {
                products = products.Where(prod => prod.Genre == genre);
            }

            if (age != Ratings.AllAges)
            {
                products = products.Where(prod => prod.Rating >= age);
            }

            products = filters.SortByPrice switch
            {
                Sorting.Asc => products.OrderBy(prod => prod.Price),
                Sorting.Desc => products.OrderByDescending(prod => prod.Price),
                _ => products
            };

            products = filters.SortByRating switch
            {
                Sorting.Asc => products.OrderBy(prod => prod.TotalRating),
                Sorting.Desc => products.OrderByDescending(prod => prod.TotalRating),
                _ => products
            };

            return products;
        }

        public IEnumerable<PlatformDTO> GetTopThreePlatforms()
        {
            var products = _productRepo.FindAll(false).ToArray();
            var sortedProducts =
                products.GroupBy(product => product.Platform)
                    .OrderByDescending(productGroupingItem => productGroupingItem.Count())
                    .Take(3)
                    .Select(productGroupingItem => new PlatformDTO()
                    {
                        Platform = productGroupingItem.Key.ToString(),
                        Games = productGroupingItem.Select(product => product.Name)
                    });

            return sortedProducts;
        }

        public IEnumerable<ProductOutputDTO> SearchProductsByTerm(string term, int limit, int offset)
        {
            if (term is null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, Messages.WrongInputData);
            }

            var products = _productRepo.FindAll(false).ToArray();
            term = term.ToLower();
            var neededProducts = 
                products.Where(prod => prod.Name.ToLower().Contains(term))
                    .Skip(offset)
                    .Take(limit)
                    .Select(prod => _mapper.Map<ProductOutputDTO>(prod));

            if (neededProducts is null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, Messages.ProductNotFound);
            }

            return neededProducts;
        }

        public ProductOutputDTO FindProductById(int id)
        {
            if (id <= 0)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, Messages.WrongInputData);
            }

            var products = _productRepo.FindAll(false).ToArray();
            var neededProduct = products.FirstOrDefault(prod => prod.Id == id);
            if (neededProduct is null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, Messages.ProductNotFound);
            }

            var productOutputDto = _mapper.Map<ProductOutputDTO>(neededProduct);

            return productOutputDto;
        }

        public async Task<ProductOutputDTO> AddProductAsync(ProductInputDTO productInputDto)
        {
            if (productInputDto is null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, Messages.WrongInputData);
            }

            var newProduct = _mapper.Map<Product>(productInputDto);
            _productRepo.Create(newProduct);
            await UploadProductImagesAsync(productInputDto, newProduct);
            if (newProduct is null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, Messages.NotCompleted);
            }

            _productRepo.Save();
            var resultDto = _mapper.Map<ProductOutputDTO>(newProduct);

            return resultDto;
        }

        public async Task<ProductOutputDTO> UpdateProductAsync(ProductInputDTO productInputDto)
        {
            if (productInputDto is null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, Messages.WrongInputData);
            }

            var products = _productRepo.FindAll(true).ToArray();
            var oldProduct = products.FirstOrDefault(prod => prod.Name == productInputDto.Name);
            if (oldProduct is null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, Messages.ProductNotFound);
            }

            var newProduct = _mapper.Map(productInputDto, oldProduct);
            await UploadProductImagesAsync(productInputDto, newProduct);
            _productRepo.Update(newProduct);
            _productRepo.Save();
            var resultDto = _mapper.Map<ProductOutputDTO>(newProduct);

            return resultDto;
        }

        public void DeleteProduct(int id)
        {
            if (id <= 0)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, Messages.WrongInputData);
            }

            var products = _productRepo.FindAll(true).ToArray();
            var deletedProduct = products.FirstOrDefault(prod => prod.Id == id);
            if (deletedProduct is null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, Messages.ProductNotFound);
            }

            _productRepo.Delete(deletedProduct);
            _productRepo.Save();
        }

        private async Task UploadProductImagesAsync(ProductInputDTO productInputDto, Product product)
        {
            if (productInputDto.Logo is not null)
            {
                var logoDownloadResult = await _cloudinary.UploadAsync(new ImageUploadParams()
                {
                    File = new FileDescription("logo" + product.Id, productInputDto.Logo.OpenReadStream())
                });

                product.Logo = logoDownloadResult.Url.AbsolutePath;
            }

            if (productInputDto.Background is not null)
            {
                var backgroundDownloadResult = await _cloudinary.UploadAsync(new ImageUploadParams()
                {
                    File = new FileDescription("background" + product.Id, productInputDto.Background.OpenReadStream())
                });

                product.Background = backgroundDownloadResult.Url.AbsolutePath;
            }
        }
    }
}