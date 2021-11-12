using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Database;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class GamesRepository : IRepository<Product>
    {
        private readonly ApplicationDbContext _db;

        public GamesRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public IQueryable<Product> GetAll()
        {
            return _db.Products;
        }
    }
}