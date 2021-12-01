using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Business.Helper
{
    public static class PagedProducts<T> where T : class
    {
        public static IEnumerable<T> ToPagedEnumerable(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var items = source.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            return items;
        }
    }
}