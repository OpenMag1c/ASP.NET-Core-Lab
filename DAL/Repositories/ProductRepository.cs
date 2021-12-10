using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Database;
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

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await FindAll()
                .OrderBy(product => product)
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await FindByCondition(product => product.Id.Equals(productId))
                .FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductWithDetailsAsync(int productId)
        {
            return await FindAllTrackChanges()
                .Include(prod => prod.Ratings)
                .FirstOrDefaultAsync(product => product.Id.Equals(productId));
        }
    }
}