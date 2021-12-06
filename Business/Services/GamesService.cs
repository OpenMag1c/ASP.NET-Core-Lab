using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.DTO;
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
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;

        public GamesService(IProductRepository productRepo, IMapper mapper, Cloudinary cloudinary)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _cloudinary = cloudinary;
        }

        public async Task<IEnumerable<ProductOutputDTO>> GetProductsAsync(Pagination pagination, ProductFilters filters)
        {
            var products = await _productRepo.GetAllProductsAsync();
            products = FilterProducts(products, filters);
            var pagedProducts = PagedProducts<Product>.ToPagedEnumerable(products, pagination.PageNumber, pagination.PageSize);
            var result = pagedProducts.Select(prod => _mapper.Map<ProductOutputDTO>(prod));
            
            return result;
        }

        private static IEnumerable<Product> FilterProducts(IEnumerable<Product> products, ProductFilters filters)
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

        public async Task<IEnumerable<PlatformDTO>> GetTopThreePlatformsAsync()
        {
            var products = await _productRepo.GetAllProductsAsync();
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

        public async Task<IEnumerable<ProductOutputDTO>> SearchProductsByTermAsync(string term, int limit, int offset)
        {
            var products = await _productRepo.GetAllProductsAsync();
            term = term.ToLower();
            var neededProducts = 
                products.Where(prod => prod.Name.ToLower().Contains(term))
                    .Skip(offset)
                    .Take(limit)
                    .Select(prod => _mapper.Map<ProductOutputDTO>(prod));

            return neededProducts;
        }

        public async Task<ProductOutputDTO> FindProductByIdAsync(int id)
        {
            var product = await _productRepo.GetProductByIdAsync(id);
            if (product is null)
            {
                return null;
            }

            var productOutputDto = _mapper.Map<ProductOutputDTO>(product);

            return productOutputDto;
        }

        public async Task<ProductOutputDTO> AddProductAsync(ProductInputDTO productInputDto)
        {
            var product = _mapper.Map<Product>(productInputDto);
            _productRepo.Create(product);
            await UploadProductImagesAsync(productInputDto, product);
            await _productRepo.SaveAsync();
            var resultDto = _mapper.Map<ProductOutputDTO>(product);

            return resultDto;
        }

        public async Task<ProductOutputDTO> UpdateProductAsync(ProductInputDTO productInputDto)
        {
            var products = await _productRepo.GetAllProductsAsync();
            var product = products.FirstOrDefault(product => product.Name == productInputDto.Name);
            if (product is null)
            {
                return null;
            }

            product = _mapper.Map(productInputDto, product);
            await UploadProductImagesAsync(productInputDto, product);
            _productRepo.Update(product);
            await _productRepo.SaveAsync();
            var resultDto = _mapper.Map<ProductOutputDTO>(product);

            return resultDto;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _productRepo.GetProductByIdAsync(id);
            if (product is null)
            {
                return false;
            }

            _productRepo.Delete(product);
            await _productRepo.SaveAsync();

            return true;
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