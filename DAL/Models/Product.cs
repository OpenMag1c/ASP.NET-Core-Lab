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
    }
}