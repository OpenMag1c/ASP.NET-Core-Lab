using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.Database;
using DAL.Interfaces;
using DAL.Models;
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

        public IQueryable<User> GetAll()
        {
            return _userManager.Users;
        }

        public Task<User> AddAsync(User item)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateAsync(User item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(User item)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}