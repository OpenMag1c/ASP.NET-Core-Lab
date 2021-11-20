using DAL.Enum;

namespace Business.Models
{
    public class ProductFilters
    {
        public Genres FilterByGenre { get; set; } = Genres.AllGenres;

        public Ratings FilterByAge { get; set; } = Ratings.AllAges;

        public Sorting SortByRating { get; set; } = Sorting.Nothing;

        public Sorting SortByPrice { get; set; } = Sorting.Nothing;
    }
}