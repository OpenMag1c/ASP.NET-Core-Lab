using System.Linq;

namespace DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAllUsers();
    }
}