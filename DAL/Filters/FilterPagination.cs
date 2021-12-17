using System.Linq;
using DAL.FilterModels;

namespace DAL.Filters
{
    public static class FilterPagination
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, Pagination pagination) =>
            source.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize);
    }
}