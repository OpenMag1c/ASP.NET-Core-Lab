using System;
using System.Linq;
using System.Linq.Expressions;
using DAL.Database;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext DbContext;

        public RepositoryBase(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
                DbContext.Set<T>()
                    .AsNoTracking() :
                DbContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
            bool trackChanges) =>
            !trackChanges ?
                DbContext.Set<T>()
                    .Where(expression)
                    .AsNoTracking() :
                DbContext.Set<T>()
                    .Where(expression);

        public void Create(T entity) => DbContext.Set<T>().Add(entity);

        public void Update(T entity) => DbContext.Set<T>().Update(entity);

        public void Delete(T entity) => DbContext.Set<T>().Remove(entity);

        public void Save() => DbContext.SaveChanges();
    }
}