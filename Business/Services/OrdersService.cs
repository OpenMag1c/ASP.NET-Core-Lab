using System;
using System.Collections.Generic;
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
    public class OrdersService : IOrdersService
    {
        private readonly IRepositoryBase<Order> _orderRepo;
        private readonly IRepositoryBase<Product> _productRepo;
        private readonly IMapper _mapper;

        public OrdersService(IRepositoryBase<Order> orderRepo, IRepositoryBase<Product> productRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<OrderOutputDTO> AddProductToOrderAsync(string userId, int productId, int amount)
        {
            var oldOrder = _orderRepo.FindAll(true)
                .Include(order => order.OrderItems)
                .FirstOrDefault(order => order.UserId == int.Parse(userId) && !order.IsPaid);
            var orderItem = new OrderItem() {ProductId = productId, Amount = amount};
            OrderOutputDTO orderDto;
            if (oldOrder is not null)
            {
                oldOrder.OrderItems.Add(orderItem);
                orderDto = _mapper.Map<OrderOutputDTO>(oldOrder);
            }
            else
            {
                var order = new Order()
                {
                    CreationDate = DateTime.Now,
                    UserId = int.Parse(userId),
                    OrderItems = new List<OrderItem>() {orderItem}
                };

                _orderRepo.Create(order);
                orderDto = _mapper.Map<OrderOutputDTO>(order);
            }

            _orderRepo.Save();

            return orderDto;
        }

        public async Task<OrderOutputDTO> GetProductsOfOrderAsync(string userId, int orderId)
        {
            var orders = _orderRepo.FindAll(false)
                .Include(order => order.OrderItems);
            Order order;
            if (orderId == 0)
            {
                order = orders.FirstOrDefault(o => o.UserId == int.Parse(userId) && !o.IsPaid);
            }
            else
            {
                order = orders.FirstOrDefault(o => o.Id == orderId);
            }

            var orderDto = _mapper.Map<OrderOutputDTO>(order);

            return orderDto;
        }

        public async Task<OrderOutputDTO> UpdateProductsOfOrderAsync(string userId, OrderItemDTO orderItemDto)
        {
            var order = GetUserOrder(userId);

            var oldOrderItem = order.OrderItems
                .FirstOrDefault(orderItem => orderItem.ProductId == orderItemDto.ProductId);
            if (oldOrderItem is not null)
            {
                oldOrderItem.Amount = orderItemDto.Amount;
                _orderRepo.Update(order);
                _orderRepo.Save();
            }

            var orderDto = _mapper.Map<OrderOutputDTO>(order);

            return orderDto;
        }

        public async Task DeleteProductsFromOrderAsync(string userId, int productId)
        {
            var order = GetUserOrder(userId);

            var oldOrderItem = order.OrderItems
                .FirstOrDefault(orderItem => orderItem.ProductId == productId);
            if (oldOrderItem is not null)
            {
                order.OrderItems.Remove(oldOrderItem);
                if (order.OrderItems.Count != 0)
                {
                    _orderRepo.Update(order);
                }
                else
                {
                    _orderRepo.Delete(order);
                }

                _orderRepo.Save();
            }
        }

        public async Task BuyProductsAsync(string userId)
        {
            var order = GetUserOrder(userId);

            var products = _productRepo.FindAll(true).ToList();
            foreach (var orderItem in order.OrderItems)
            {
                var product = products.FirstOrDefault(product => product.Id == orderItem.ProductId);
                if (product is not null && product.Count - orderItem.Amount >= 0)
                {
                    product.Count -= orderItem.Amount;
                }
            }

            order.IsPaid = true;
            _orderRepo.Update(order);
            _productRepo.Save();
        }

        private Order GetUserOrder(string userId)
        {
            var order = _orderRepo.FindAll(true)
                .Include(order => order.OrderItems)
                .FirstOrDefault(order => order.UserId == int.Parse(userId) && !order.IsPaid);
            if (order is null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Product is paid");
            }

            return order;
        }
    }
}