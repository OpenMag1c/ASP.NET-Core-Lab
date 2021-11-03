using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Database;
using DAL.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : IRepository<IdentityUser<int>>, IDisposable
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext context)
        {
            this._db = context;
        }

        public IEnumerable<IdentityUser<int>> GetAll()
        {
            return _db.Users;
        }

        public IdentityUser<int> Get(int id)
        {
            return _db.Users.Find(id);
        }

        public IEnumerable<IdentityUser<int>> Find(Func<IdentityUser<int>, bool> predicate)
        {
            return _db.Users.Where(predicate).ToList();
        }

        public void Create(IdentityUser<int> item)
        {
            _db.Users.Add(item);
            _db.SaveChanges();
        }

        public void Update(IdentityUser<int> item)
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