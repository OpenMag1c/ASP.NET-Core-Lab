using System.Threading.Tasks;
using Business.DTO;

namespace Business.Interfaces
{
    public interface IOrdersService
    {
        public Task<OrderOutputDTO> AddProductToOrderAsync(string userId, int productId, int amount);
        public Task<OrderOutputDTO> GetProductsOfOrderAsync(string userId);
        public Task<OrderOutputDTO> GetProductsOfOrderAsync(int orderId);
        public Task<OrderOutputDTO> UpdateProductsOfOrderAsync(string userId, OrderItemDTO orderItemDto);
        public Task DeleteProductsFromOrderAsync(string userId, int productId);
        public Task BuyProductsAsync(string userId);
    }
}