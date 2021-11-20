using DAL.Enum;

namespace Business.Models
{
    public class ProductFilters
    {
        public Genres FilterByGenre { get; set; } = Genres.AllGenres;

        public Ratings FilterByAge { get; set; } = Ratings.AllAges;

        public AscDesc SortByRating { get; set; } = AscDesc.Nothing;

        public AscDesc SortByPrice { get; set; } = AscDesc.Nothing;
    }
}