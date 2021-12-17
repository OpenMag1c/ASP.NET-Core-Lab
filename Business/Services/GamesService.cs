using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.DTO;
using Business.Interfaces;
using DAL.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DAL.Interfaces;
using DAL.FilterModels;

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
            var products = await _productRepo.GetFilteredProductsAsync(filters, pagination);
            var result = products.Select(prod => _mapper.Map<ProductOutputDTO>(prod));
            
            return result;
        }

        public async Task<IEnumerable<PlatformDTO>> GetTopThreePlatformsAsync()
        {
            var topThreePlatforms = await _productRepo.GetTopThreePlatformProductsAsync();
            var platformsDto = topThreePlatforms
                .Select(platform => new PlatformDTO()
                {
                    Platform = platform.ToString(),
                    Games = _productRepo.GetProductNamesByPlatformAsync(platform).Result
                });

            return platformsDto;
        }

        public async Task<IEnumerable<ProductOutputDTO>> SearchProductsByTermAsync(string term, int limit, int offset)
        {
            term = term.ToLower();
            var neededProducts = await _productRepo.GetProductsByTermAsync(term, limit, offset);
            var productsDto = neededProducts.Select(prod => _mapper.Map<ProductOutputDTO>(prod));

            return productsDto;
        }

        public async Task<ProductOutputDTO> FindProductByIdAsync(int id)
        {
            var product = await _productRepo.GetProductByIdAsync(id);
            if (product is null)
            {
                return await Task.FromResult<ProductOutputDTO>(null);
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
                return await Task.FromResult<ProductOutputDTO>(null);
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