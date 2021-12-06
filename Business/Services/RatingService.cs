using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business.DTO;
using Business.ExceptionMiddleware;
using Business.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace Business.Services
{
    public class RatingService : IRatingService
    {
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;

        public RatingService(IProductRepository productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<ProductOutputDTO> EditProductRatingAsync(string userId, int productId, int rating)
        {
            var product = await _productRepo.GetProductWithDetailsAsync(productId);
            if (product is null)
            {
                return null;
            }

            var productRating = product.Ratings.FirstOrDefault(rate => rate.UserId == int.Parse(userId));
            if (productRating is not null)
            {
                productRating.Rating = rating;
            }
            else
            {
                productRating = new ProductRating
                {
                    Product = product, UserId = int.Parse(userId), Rating = rating
                };
                product.Ratings.Add(productRating);
            }

            _productRepo.Update(product);
            await _productRepo.SaveAsync();
            await RecalculateTotalRating(product);
            var productOutputDto = _mapper.Map<ProductOutputDTO>(product);

            return productOutputDto;
        }

        public async Task<bool> DeleteRatingAsync(string userId, int productId)
        {
            var product = await _productRepo.GetProductWithDetailsAsync(productId);
            if (product is null)
            {
                return false;
            }

            var ratings = product.Ratings;
            var productRating = ratings.FirstOrDefault(rating => rating.UserId == int.Parse(userId));
            if (productRating is null)
            {
                return false;
            }

            ratings.Remove(productRating);
            await _productRepo.SaveAsync();
            await RecalculateTotalRating(product);
            return true;
        }

        public async Task RecalculateTotalRating(Product product)
        {
            var totalRating = (int)product.Ratings
                .Average(rate => rate.Rating);
            product.TotalRating = totalRating;
            _productRepo.Update(product);
            await _productRepo.SaveAsync();
        }
    }
}