using System.Linq;
using System.Net;
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

        public ProductOutputDTO EditProductRating(string userId, int productId, int rating)
        {
            var product = _productRepo.FindAll(true)
                .FirstOrDefault(prod => prod.Id == productId);
            if (product is null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, Messages.ProductNotFound);
            }

            var ratings = _ratingRepo.FindAll(true);
            var productRating = ratings.FirstOrDefault(rate => rate.UserId == int.Parse(userId) && rate.ProductId == productId);
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

        public void DeleteRating(string userId, int productId)
        {
            var product = _productRepo.FindAll(true)
                .FirstOrDefault(prod => prod.Id == productId);
            if (product is null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, Messages.ProductNotFound);
            }

            var ratings = _ratingRepo.FindAll(true).Include(rate => rate.Product);
            var productRating = ratings.FirstOrDefault(rating => rating.UserId == int.Parse(userId) && rating.ProductId == productId);
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
            var newRatings = _ratingRepo.FindAll(false);
            var totalRating = (int)newRatings.Where(rate => rate.ProductId == product.Id)
                .Average(rate => rate.Rating);
            product.TotalRating = totalRating;
            _productRepo.Save();
        }
    }
}