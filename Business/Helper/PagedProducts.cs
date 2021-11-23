using System.Collections.Generic;
using System.Linq;

namespace Business.Helper
{
    public static class PagedProducts<T> where T : class
    {
        public static List<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var items = source.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return items;
        }
    }
}