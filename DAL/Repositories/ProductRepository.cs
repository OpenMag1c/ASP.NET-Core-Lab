using DAL.Database;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(ApplicationDbContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}