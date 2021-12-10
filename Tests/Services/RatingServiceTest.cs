using System.Linq;
using System.Threading.Tasks;
using Business.Services;
using DAL.Interfaces;
using DAL.Models;
using FakeItEasy;
using WebAPI.Tests.Extensions;
using Xunit;

namespace WebAPI.Tests.Services
{
    public class RatingServiceTest : Tester
    {
        [Fact]
        public async Task EditProductRatingAsync_ReturnsProduct()
        {
            // Arrange
            var userId = "0";
            var fakeProductRepo = A.Fake<IProductRepository>();
            var product = CreateEnumerable<Product>().First();
            var ratings = CreateEnumerable<ProductRating>().ToList();
            product.Ratings = ratings;
            var ratingService = new RatingService(fakeProductRepo, Mapper);
            A.CallTo(() => fakeProductRepo.GetProductWithDetailsAsync(A<int>.Ignored))
                .Returns(Task.FromResult(product));

            // Act
            var result = await ratingService.EditProductRatingAsync(userId, 0, 0);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task EditProductRatingAsync_ReturnsNullIfProductNotFound()
        {
            // Arrange
            var userId = "0";
            var fakeProductRepo = A.Fake<IProductRepository>();
            var ratingService = new RatingService(fakeProductRepo, Mapper);
            A.CallTo(() => fakeProductRepo.GetProductWithDetailsAsync(A<int>.Ignored))
                .Returns(Task.FromResult<Product>(null));

            // Act
            var result = await ratingService.EditProductRatingAsync(userId, 0, 0);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteRatingAsync_ReturnsTrueIfProductDeleted()
        {
            // Arrange
            var userId = "0";
            var fakeProductRepo = A.Fake<IProductRepository>();
            var product = CreateEnumerable<Product>().First();
            product.Ratings.Add(new ProductRating()
            {
                UserId = int.Parse(userId)
            });
            var ratingService = new RatingService(fakeProductRepo, Mapper);
            A.CallTo(() => fakeProductRepo.GetProductWithDetailsAsync(A<int>.Ignored))
                .Returns(Task.FromResult(product));

            // Act
            var result = await ratingService.DeleteRatingAsync(userId, 0);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteRatingAsync_ReturnsFalseIfProductRatingNotFound()
        {
            // Arrange
            var userId = "0";
            var fakeProductRepo = A.Fake<IProductRepository>();
            var product = CreateEnumerable<Product>().First();
            var ratingService = new RatingService(fakeProductRepo, Mapper);
            A.CallTo(() => fakeProductRepo.GetProductWithDetailsAsync(A<int>.Ignored))
                .Returns(Task.FromResult(product));

            // Act
            var result = await ratingService.DeleteRatingAsync(userId, 0);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteRatingAsync_ReturnsFalseIfProductNotFound()
        {
            // Arrange
            var userId = "0";
            var fakeProductRepo = A.Fake<IProductRepository>();
            var ratingService = new RatingService(fakeProductRepo, Mapper);
            A.CallTo(() => fakeProductRepo.GetProductWithDetailsAsync(A<int>.Ignored))
                .Returns(Task.FromResult<Product>(null));

            // Act
            var result = await ratingService.DeleteRatingAsync(userId, 0);

            // Assert
            Assert.False(result);
        }
    }
}