using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Business.Helper
{
    public static class PagedProducts<T> where T : class
    {
        public static async Task<List<T>> ToPagedListAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var items = await source.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return items;
        }
    }
}