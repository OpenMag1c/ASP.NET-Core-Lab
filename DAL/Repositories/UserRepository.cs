using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.UserContext;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class UserRepository : IRepository
    {
        private UserDbContext db;

        public UserRepository(UserDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        public void Create(User item)
        {
            db.Users.Add(item);
            db.SaveChanges();
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var book = db.Users.Find(id);
            if (book != null)
                db.Users.Remove(book);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}