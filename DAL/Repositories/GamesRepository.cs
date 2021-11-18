using System.Linq;
using System.Threading.Tasks;
using DAL.Database;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class GamesRepository : IRepository<Product>
    {
        private readonly ApplicationDbContext _db;

        public GamesRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public IQueryable<Product> GetAll()
        {
            return _db.Products;
        }

        public async Task<Product> AddAsync(Product item)
        {
            await _db.Products.AddAsync(item);
            await _db.SaveChangesAsync();

            return item;
        }

        public async Task<Product> UpdateAsync(Product item)
        {
            _db.Update(item);
            await _db.SaveChangesAsync();

            return item;
        }

        public async Task<bool> DeleteAsync(Product item)
        {
            try
            {
                _db.Remove(item);
                await _db.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}