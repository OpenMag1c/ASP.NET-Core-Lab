using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Database;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await FindAll()
                .ToListAsync();
        }

        public async Task<Order> GetUserOrderAsync(string userId)
        {
            return await FindByCondition(order => order.UserId.Equals(int.Parse(userId)))
                .FirstOrDefaultAsync();
        }

        public async Task<Order> GetUserOrderWithDetailsAsync(string userId)
        {
            return await FindByCondition(order => order.UserId.Equals(int.Parse(userId)))
                .Include(order => order.OrderItems)
                .FirstOrDefaultAsync();
        }

        public async Task<Order> GetOrderByIdWithDetailsAsync(int orderId)
        {
            return await FindByCondition(order => order.Id == orderId)
                .Include(order => order.OrderItems)
                .FirstOrDefaultAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await FindByCondition(order => order.Id == orderId)
                .FirstOrDefaultAsync();
        }

        public async Task<Order> GetUnpaidUserOrderAsync(string userId)
        {
            return await FindAllTrackChanges()
                .Include(order => order.OrderItems)
                .FirstOrDefaultAsync(order => order.UserId == int.Parse(userId) && !order.IsPaid);
        }
    }
}