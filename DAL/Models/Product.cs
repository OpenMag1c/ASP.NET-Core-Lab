using DAL.Enum;

namespace DAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Platforms Platform { get; set; }
        public int DateCreated { get; set; }
        public int TotalRating { get; set; }
        public string Genre { get; set; }
        public Ratings Rating { get; set; }
        public string Logo { get; set; }
        public string Background { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    }
}