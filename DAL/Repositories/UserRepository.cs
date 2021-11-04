using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Database;
using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public User Get(int id)
        {
            return _db.Users.Find(id);
        }

        public IQueryable<User> GetAllUsers()
        {
            return _userManager.Users;
        }

        public Task<User> FindUserByNameAsync(string name)
        {
            var result = _userManager.FindByNameAsync(name);
            return result;
        }

        public void Dispose()
        {
            _db?.Dispose();
        }

        public void RegisterUser(User user)
        {
            // добавляем пользователя
            _userManager.CreateAsync(user, user.PasswordHash);
            _db.SaveChanges();
        }
    }
}