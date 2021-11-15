using System.Linq;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        void RegisterUser(T user);
        T Get(int id);
        IQueryable<T> GetAllUsers();
        //Task<User> FindUserByName(string name);
    }
}