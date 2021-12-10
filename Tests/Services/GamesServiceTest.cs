using System.Linq;
using System.Threading.Tasks;
using Business.DTO;
using Business.Models;
using Business.Services;
using DAL.Interfaces;
using DAL.Models;
using FakeItEasy;
using WebAPI.Tests.Extensions;
using Xunit;

namespace WebAPI.Tests.Services
{
    public class GamesServiceTest : Tester
    {
        [Fact]
        public async Task GetProductsAsync_ReturnsProducts()
        {
            // Arrange
            var fakeProductRepo = A.Fake<IProductRepository>();
            var products = CreateEnumerable<Product>(10);
            var filters = new ProductFilters();
            var pagination = new Pagination();
            var gamesService = new GamesService(fakeProductRepo, Mapper, null);
            A.CallTo(() => fakeProductRepo.GetAllProductsAsync())
                .Returns(Task.FromResult(products));

            // Act
            var result = await gamesService.GetProductsAsync(pagination, filters);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetTopThreePlatformsAsync_ReturnsPlatforms()
        {
            // Arrange
            var fakeProductRepo = A.Fake<IProductRepository>();
            var products = CreateEnumerable<Product>(10);
            var gamesService = new GamesService(fakeProductRepo, Mapper, null);
            A.CallTo(() => fakeProductRepo.GetAllProductsAsync())
                .Returns(Task.FromResult(products));

            // Act
            var result = await gamesService.GetTopThreePlatformsAsync();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task SearchProductsByTermAsync_ReturnsProducts()
        {
            // Arrange
            var fakeProductRepo = A.Fake<IProductRepository>();
            var products = CreateEnumerable<Product>(10);
            var gamesService = new GamesService(fakeProductRepo, Mapper, null);
            A.CallTo(() => fakeProductRepo.GetAllProductsAsync())
                .Returns(Task.FromResult(products));

            // Act
            var result = await gamesService.SearchProductsByTermAsync(string.Empty, 0, 0);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task FindProductByIdAsync_ReturnsProduct()
        {
            // Arrange
            var fakeProductRepo = A.Fake<IProductRepository>();
            var product = CreateEnumerable<Product>(10).First();
            var gamesService = new GamesService(fakeProductRepo, Mapper, null);
            A.CallTo(() => fakeProductRepo.GetProductByIdAsync(A<int>.Ignored))
                .Returns(Task.FromResult(product));

            // Act
            var result = await gamesService.FindProductByIdAsync(0);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task FindProductByIdAsync_ReturnsNullIfProductNotFound()
        {
            // Arrange
            var fakeProductRepo = A.Fake<IProductRepository>();
            var gamesService = new GamesService(fakeProductRepo, Mapper, null);
            A.CallTo(() => fakeProductRepo.GetProductByIdAsync(A<int>.Ignored))
                .Returns(Task.FromResult<Product>(null));

            // Act
            var result = await gamesService.FindProductByIdAsync(0);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddProductAsync_ReturnsProduct()
        {
            // Arrange
            var fakeProductRepo = A.Fake<IProductRepository>();
            var fakeProductInputDto = A.Fake<ProductInputDTO>();
            var products = CreateEnumerable<Product>(10);
            var gamesService = new GamesService(fakeProductRepo, Mapper, null);
            A.CallTo(() => fakeProductRepo.GetAllProductsAsync())
                .Returns(Task.FromResult(products));

            // Act
            var result = await gamesService.AddProductAsync(fakeProductInputDto);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateProductAsync_ReturnsProduct()
        {
            // Arrange
            var fakeProductRepo = A.Fake<IProductRepository>();
            var fakeProductInputDto = A.Fake<ProductInputDTO>();
            var product = Mapper.Map<Product>(fakeProductInputDto);
            var products = CreateEnumerable<Product>(10);
            products = products.Append(product);
            var gamesService = new GamesService(fakeProductRepo, Mapper, null);
            A.CallTo(() => fakeProductRepo.GetAllProductsAsync())
                .Returns(Task.FromResult(products));

            // Act
            var result = await gamesService.UpdateProductAsync(fakeProductInputDto);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateProductAsync_ReturnsNullIfProductNotFound()
        {
            // Arrange
            var fakeProductRepo = A.Fake<IProductRepository>();
            var fakeProductInputDto = A.Fake<ProductInputDTO>();
            var products = CreateEnumerable<Product>(10);
            var gamesService = new GamesService(fakeProductRepo, Mapper, null);
            A.CallTo(() => fakeProductRepo.GetAllProductsAsync())
                .Returns(Task.FromResult(products));

            // Act
            var result = await gamesService.UpdateProductAsync(fakeProductInputDto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteProductAsync_ReturnsTrueIfProductDeleted()
        {
            // Arrange
            var fakeProductRepo = A.Fake<IProductRepository>();
            var products = CreateEnumerable<Product>(10);
            var gamesService = new GamesService(fakeProductRepo, Mapper, null);
            A.CallTo(() => fakeProductRepo.GetProductByIdAsync(A<int>.Ignored))
                .Returns(Task.FromResult(products.First()));

            // Act
            var result = await gamesService.DeleteProductAsync(0);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteProductAsync_ReturnsFalseIfProductNotFound()
        {
            // Arrange
            var fakeProductRepo = A.Fake<IProductRepository>();
            var gamesService = new GamesService(fakeProductRepo, Mapper, null);
            A.CallTo(() => fakeProductRepo.GetProductByIdAsync(A<int>.Ignored))
                .Returns(Task.FromResult<Product>(null));

            // Act
            var result = await gamesService.DeleteProductAsync(0);

            // Assert
            Assert.False(result);
        }
    }
}