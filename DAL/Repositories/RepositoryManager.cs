using DAL.Database;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _repositoryContext;
        private ProductRepository _productRepository;

        public RepositoryManager(ApplicationDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public ProductRepository Product
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_repositoryContext);
                return _productRepository;
            }
        }

        public void Save() => _repositoryContext.SaveChanges();
    }

}