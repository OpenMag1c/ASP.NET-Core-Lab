using System;
using System.Linq;
using System.Threading.Tasks;
using Business.DTO;
using Business.Services;
using DAL.Interfaces;
using DAL.Models;
using FakeItEasy;
using WebAPI.Tests.Extensions;
using Xunit;

namespace WebAPI.Tests.Services
{
    public class OrderServiceTest : Tester
    {
        [Fact]
        public async Task AddProductToOrderAsync_ReturnsOrder()
        {
            // Arrange
            var fakeOrderRepo = A.Fake<IOrderRepository>();
            var orders = CreateEnumerable<Order>();
            var ordersService = new OrdersService(fakeOrderRepo, Mapper);
            A.CallTo(() => fakeOrderRepo.GetUnpaidUserOrderAsync(null)).WithAnyArguments()
                .Returns(Task.FromResult(orders.First()));

            // Act
            var result = await ordersService.AddProductToOrderAsync(string.Empty, 0, 0);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetProductsOfOrderAsync_GetUserId_ReturnsOrder()
        {
            // Arrange
            var fakeOrderRepo = A.Fake<IOrderRepository>();
            var orders = CreateEnumerable<Order>();
            var ordersService = new OrdersService(fakeOrderRepo, Mapper);
            A.CallTo(() => fakeOrderRepo.GetUnpaidUserOrderAsync(null)).WithAnyArguments()
                .Returns(Task.FromResult(orders.First()));

            // Act
            var result = await ordersService.GetProductsOfOrderAsync(string.Empty);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetProductsOfOrderAsync_GetOrderId_ReturnsOrder()
        {
            // Arrange
            var fakeOrderRepo = A.Fake<IOrderRepository>();
            var orders = CreateEnumerable<Order>();
            var ordersService = new OrdersService(fakeOrderRepo, Mapper);
            A.CallTo(() => fakeOrderRepo.GetUnpaidUserOrderAsync(null)).WithAnyArguments()
                .Returns(Task.FromResult(orders.First()));

            // Act
            var result = await ordersService.GetProductsOfOrderAsync(0);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateProductsOfOrderAsync_ReturnsOrder()
        {
            // Arrange
            var fakeOrderRepo = A.Fake<IOrderRepository>();
            var order = CreateEnumerable<Order>().First();
            var orderItem = new OrderItem();
            order.OrderItems.Add(orderItem);
            var orderItemDto = new OrderItemDTO()
            {
                ProductId = orderItem.ProductId,
                Amount = orderItem.Amount
            };
            var ordersService = new OrdersService(fakeOrderRepo, Mapper);
            A.CallTo(() => fakeOrderRepo.GetUserOrderAsync(null)).WithAnyArguments()
                .Returns(Task.FromResult(order));

            // Act
            var result = await ordersService.UpdateProductsOfOrderAsync(String.Empty, orderItemDto);

            // Assert
            Assert.NotNull(result);
        }
    }
}