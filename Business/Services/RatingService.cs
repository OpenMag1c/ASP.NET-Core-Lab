using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business.DTO;
using Business.ExceptionMiddleware;
using Business.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRepositoryBase<Product> _productRepo;
        private readonly IRepositoryBase<ProductRating> _ratingRepo;
        private readonly IMapper _mapper;

        public RatingService(IRepositoryBase<Product> productRepo, IRepositoryBase<ProductRating> ratingRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _ratingRepo = ratingRepo;
            _mapper = mapper;
        }

        public async Task<ProductOutputDTO> EditProductRatingAsync(string userId, int productId, int rating)
        {
            var product = await _productRepo.FindAll(true)
                .FirstOrDefaultAsync(prod => prod.Id == productId);
            if (product is null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, Messages.ProductNotFound);
            }

            var ratings = _ratingRepo.FindAll(true);
            var productRating = await ratings.FirstOrDefaultAsync(rate => rate.UserId == int.Parse(userId) && rate.ProductId == productId);
            if (productRating is not null)
            {
                productRating.Rating = rating;
            }
            else
            {
                productRating = new ProductRating
                    {Product = product, UserId = int.Parse(userId), Rating = rating};
                _ratingRepo.Create(productRating);
            }

            _ratingRepo.Save();
            RecalculateTotalRating(product);
            var productOutputDto = _mapper.Map<ProductOutputDTO>(product);

            return productOutputDto;
        }

        public async Task DeleteRatingAsync(string userId, int productId)
        {
            var product = await _productRepo.FindAll(true)
                .FirstOrDefaultAsync(prod => prod.Id == productId);
            if (product is null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, Messages.ProductNotFound);
            }

            var ratings = _ratingRepo.FindAll(true).Include(rate => rate.Product);
            var productRating = await ratings.FirstOrDefaultAsync(rating => rating.UserId == int.Parse(userId) && rating.ProductId == productId);
            if (productRating is null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, Messages.NotCompleted);
            }

            _ratingRepo.Delete(productRating);
            _ratingRepo.Save();
            RecalculateTotalRating(product);
        }

        public void RecalculateTotalRating(Product product)
        {
            var ratings = _ratingRepo.FindAll(false);
            var totalRating = (int)ratings.Where(rate => rate.ProductId == product.Id)
                .Average(rate => rate.Rating);
            product.TotalRating = totalRating;
            _productRepo.Save();
        }
    }
}