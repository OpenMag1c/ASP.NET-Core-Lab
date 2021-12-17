using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.FilterModels;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetFilteredProductsAsync(ProductFilters filters, Pagination pagination);
        Task<IEnumerable<Enum.Platforms>> GetTopThreePlatformProductsAsync();
        Task<IEnumerable<Product>> GetProductsByTermAsync(string term, int limit, int offset);
        Task<IEnumerable<string>> GetProductNamesByPlatformAsync(Enum.Platforms platform);
        Task<Product> GetProductByIdAsync(int productId);
        Task<Product> GetProductWithDetailsAsync(int productId);
    }
}