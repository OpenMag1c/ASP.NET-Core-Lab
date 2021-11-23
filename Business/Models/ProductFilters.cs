using DAL.Enum;

namespace Business.Models
{
    public class ProductFilters
    {
        public Genres FilterByGenre { get; set; }

        public Ratings FilterByAge { get; set; }

        public Sorting SortByRating { get; set; }

        public Sorting SortByPrice { get; set; }
    }
}