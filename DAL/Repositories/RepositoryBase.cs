using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Database;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext DbContext;

        public RepositoryBase(ApplicationDbContext repositoryContext)
        {
            DbContext = repositoryContext;
        }

        public IQueryable<T> FindAll() => DbContext.Set<T>().AsNoTracking();

        public IQueryable<T> FindAllTrackChanges() => DbContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => DbContext.Set<T>().Where(expression).AsNoTracking();
        
        public void Create(T entity) => DbContext.Set<T>().Add(entity);
        
        public void Update(T entity) => DbContext.Set<T>().Update(entity);
        
        public void Delete(T entity) => DbContext.Set<T>().Remove(entity);

        public async Task SaveAsync() => await DbContext.SaveChangesAsync();
    }
}