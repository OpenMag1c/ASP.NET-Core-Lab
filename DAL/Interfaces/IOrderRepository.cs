using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetUserOrderAsync(string userId);
        Task<Order> GetUserOrderWithDetailsAsync(string userId);
        Task<Order> GetOrderByIdWithDetailsAsync(int orderId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<Order> GetUnpaidUserOrderAsync(string userId);
    }
}