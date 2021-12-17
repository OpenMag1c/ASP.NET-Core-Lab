using System.ComponentModel.DataAnnotations;
using DAL.Enum;

namespace DAL.FilterModels
{
    public class ProductFilters
    {
        [EnumDataType(typeof(Genres))]
        public Genres FilterByGenre { get; set; }

        [EnumDataType(typeof(Ratings))]
        public Ratings FilterByAge { get; set; }

        [EnumDataType(typeof(Sorting))]
        public Sorting SortByRating { get; set; }

        [EnumDataType(typeof(Sorting))]
        public Sorting SortByPrice { get; set; }
    }
}