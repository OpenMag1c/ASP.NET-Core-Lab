using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Database;
using DAL.Models;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : IRepository<User>, IDisposable
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext context)
        {
            this._db = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _db.Users;
        }

        public User Get(int id)
        {
            return _db.Users.Find(id);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return _db.Users.Where(predicate).ToList();
        }

        public void Create(User item)
        {
            _db.Users.Add(item);
            _db.SaveChanges();
        }

        public void Update(User item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var book = _db.Users.Find(id);
            if (book != null)
                _db.Users.Remove(book);
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}