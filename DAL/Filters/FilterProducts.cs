using System.Collections.Generic;
using System.Linq;
using DAL.Enum;
using DAL.FilterModels;
using DAL.Models;

namespace DAL.Filters
{
    public static class FilterProducts
    {
        public static IQueryable<Product> Filter(this IQueryable<Product> products, ProductFilters filters)
        {
            var genre = filters.FilterByGenre;
            var age = filters.FilterByAge;
            if (genre != Genres.AllGenres)
            {
                products = products.Where(prod => prod.Genre == genre);
            }

            if (age != Ratings.AllAges)
            {
                products = products.Where(prod => prod.Rating >= age);
            }

            products = filters.SortByPrice switch
            {
                Sorting.Asc => products.OrderBy
                    (prod => prod.Price),
                Sorting.Desc => products.OrderByDescending(prod => prod.Price),
                _ => products
            };

            products = filters.SortByRating switch
            {
                Sorting.Asc => products.OrderBy(prod => prod.TotalRating),
                Sorting.Desc => products.OrderByDescending(prod => prod.TotalRating),
                _ => products
            };

            return products;
        }
    }
}