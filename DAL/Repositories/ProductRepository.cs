using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Database;
using DAL.Enum;
using DAL.FilterModels;
using DAL.Filters;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync() => await FindAll()
            .OrderBy(product => product)
            .ToListAsync();

        public async Task<IEnumerable<Product>> GetFilteredProductsAsync(ProductFilters filters, Pagination pagination) => await FindAll()
            .Filter(filters).Paginate(pagination).ToListAsync();

        public async Task<IEnumerable<Enum.Platforms>> GetTopThreePlatformProductsAsync() => await FindAll()
            .GroupBy(product => product.Platform)
            .OrderByDescending(productGroupingItem => productGroupingItem.Count())
            .Take(3)
            .Select(g => g.Key)
            .ToListAsync();

        public async Task<IEnumerable<Product>> GetProductsByTermAsync(string term, int limit, int offset) => await FindAll()
            .Where(prod => prod.Name.ToLower().Contains(term))
            .Skip(offset)
            .Take(limit).ToListAsync();

        public async Task<IEnumerable<string>> GetProductNamesByPlatformAsync(Platforms platform) => await FindAll()
            .Where(product => product.Platform == platform)
            .Select(product => product.Name)
            .ToListAsync();

        public async Task<Product> GetProductByIdAsync(int productId) => await FindByCondition(product => product.Id.Equals(productId))
            .FirstOrDefaultAsync();

        public async Task<Product> GetProductWithDetailsAsync(int productId) => await FindAllTrackChanges()
            .Include(prod => prod.Ratings)
            .FirstOrDefaultAsync(product => product.Id.Equals(productId));
    }
}