using DAL.Repositories;

namespace DAL.Interfaces
{
    public interface IRepositoryManager
    {
        ProductRepository Product { get; }
        void Save();
    }
}