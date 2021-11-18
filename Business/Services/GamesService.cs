using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business.DTO;
using Business.ExceptionMiddleware;
using Business.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DAL.Interfaces;
using DAL.Models;

namespace Business.Services
{
    public class GamesService : IGamesService
    {
        private readonly IRepository<Product> _repo;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;

        public GamesService(IRepository<Product> repo, IMapper mapper, Cloudinary cloudinary)
        {
            _repo = repo;
            _mapper = mapper;
            _cloudinary = cloudinary;
        }

        public IEnumerable<PlatformDTO> GetTopThreePlatforms()
        {
            var products = _repo.GetAll().ToArray();
            var sortedProducts =
                products.GroupBy(product => product.Platform)
                    .OrderByDescending(productGroupingItem => productGroupingItem.Count())
                    .Take(3)
                    .Select(productGroupingItem => new PlatformDTO()
                    {
                        Name = productGroupingItem.Key.ToString(),
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

            var products = _repo.GetAll().ToArray();
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

            var products = _repo.GetAll().ToArray();
            var neededProduct = _mapper.Map<ProductOutputDTO>(products.FirstOrDefault(prod => prod.Id == id));
            if (neededProduct is null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, Messages.ProductNotFound);
            }

            return neededProduct;
        }

        public async Task<ProductOutputDTO> AddProductAsync(ProductInputDTO productInputDto)
        {
            if (productInputDto is null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, Messages.WrongInputData);
            }

            var newProduct = _mapper.Map<Product>(productInputDto);
            var result = await _repo.AddAsync(newProduct);
            await UploadProductImagesAsync(productInputDto, result);
            await _repo.UpdateAsync(result);
            if (result is null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, Messages.NotCompleted);
            }

            var resultDto = _mapper.Map<ProductOutputDTO>(result);

            return resultDto;
        }

        public async Task<ProductOutputDTO> UpdateProductAsync(ProductInputDTO productInputDto)
        {
            if (productInputDto is null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, Messages.WrongInputData);
            }

            var products = _repo.GetAll().ToArray();
            var oldProduct = products.FirstOrDefault(prod => prod.Name == productInputDto.Name);
            if (oldProduct is null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, Messages.ProductNotFound);
            }

            var newProduct = _mapper.Map(productInputDto, oldProduct);
            await UploadProductImagesAsync(productInputDto, newProduct);
            var result = await _repo.UpdateAsync(newProduct);
            var resultDto = _mapper.Map<ProductOutputDTO>(result);

            return resultDto;
        }

        public async void DeleteProductAsync(int id)
        {
            if (id <= 0)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, Messages.WrongInputData);
            }

            var products = _repo.GetAll().ToArray();
            var deletedProduct = products.FirstOrDefault(prod => prod.Id == id);
            if (deletedProduct is null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, Messages.ProductNotFound);
            }

            var result = await _repo.DeleteAsync(deletedProduct);
            if (!result)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, Messages.NotCompleted);
            }
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