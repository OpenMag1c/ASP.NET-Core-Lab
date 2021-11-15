using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.Database;
using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Identity;

namespace DAL.Repositories
{
    public class UserRepository : IRepository<User>, IDisposable
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager, ApplicationDbContext context)
        {
            _db = context;
            _userManager = userManager;
        }

        public IQueryable<User> GetAllUsers()
        {
            return _userManager.Users;
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}