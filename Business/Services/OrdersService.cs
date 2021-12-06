using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.DTO;
using Business.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace Business.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IMapper _mapper;

        public OrdersService(IOrderRepository orderRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
        }

        public async Task<OrderOutputDTO> AddProductToOrderAsync(string userId, int productId, int amount)
        {
            var currentOrder = await _orderRepo.GetUnpaidUserOrderAsync(userId);
            var orderItem = new OrderItem() {ProductId = productId, Amount = amount};
            var orderOutputDto = await CreateOrUpdateOrder(currentOrder, orderItem, userId);

            return orderOutputDto;
        }

        public async Task<OrderOutputDTO> GetProductsOfOrderAsync(string userId)
        {
            var order = await _orderRepo.GetUnpaidUserOrderAsync(userId);
            var orderDto = _mapper.Map<OrderOutputDTO>(order);

            return orderDto;
        }

        public async Task<OrderOutputDTO> GetProductsOfOrderAsync(int orderId)
        {
            var order = await _orderRepo.GetOrderByIdWithDetailsAsync(orderId);
            var orderDto = _mapper.Map<OrderOutputDTO>(order);

            return orderDto;
        }

        public async Task<OrderOutputDTO> UpdateProductsOfOrderAsync(string userId, OrderItemDTO orderItemDto)
        {
            var order = await _orderRepo.GetUserOrderAsync(userId);

            var currentOrderItem = order.OrderItems
                .FirstOrDefault(orderItem => orderItem.ProductId == orderItemDto.ProductId);
            if (currentOrderItem is not null)
            {
                currentOrderItem.Amount = orderItemDto.Amount;
                _orderRepo.Update(order);
                await _orderRepo.SaveAsync();
            }

            var orderDto = _mapper.Map<OrderOutputDTO>(order);

            return orderDto;
        }

        public async Task DeleteProductsFromOrderAsync(string userId, int productId)
        {
            var order = await GetUserOrderAsync(userId);

            var currentOrderItem = order.OrderItems
                .FirstOrDefault(orderItem => orderItem.ProductId == productId);
            if (currentOrderItem is not null)
            {
                order.OrderItems.Remove(currentOrderItem);
                if (order.OrderItems.Count != 0)
                {
                    _orderRepo.Update(order);
                }
                else
                {
                    _orderRepo.Delete(order);
                }

                await _orderRepo.SaveAsync();
            }
        }

        public async Task BuyProductsAsync(string userId)
        {
            var order = await GetUserOrderAsync(userId);
            order.IsPaid = true;
            _orderRepo.Update(order);
            await _orderRepo.SaveAsync();
        }

        private async Task<Order> GetUserOrderAsync(string userId)
        {
            var order = await _orderRepo.GetUnpaidUserOrderAsync(userId);

            return order;
        }

        private async Task<OrderOutputDTO> CreateOrUpdateOrder(Order currentOrder, OrderItem orderItem, string userId)
        {
            OrderOutputDTO orderDto;
            if (currentOrder is not null)
            {
                currentOrder.OrderItems.Add(orderItem);
                orderDto = _mapper.Map<OrderOutputDTO>(currentOrder);
            }
            else
            {
                var order = new Order()
                {
                    CreationDate = DateTime.Now,
                    UserId = int.Parse(userId),
                    OrderItems = new List<OrderItem>() { orderItem }
                };

                _orderRepo.Create(order);
                orderDto = _mapper.Map<OrderOutputDTO>(order);
            }

            _orderRepo.Update(currentOrder);
            await _orderRepo.SaveAsync();

            return orderDto;
        }
    }
}