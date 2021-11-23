using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DAL.Models
{
    public class User : IdentityUser<int>
    {
        public int Age { get; set; }
        public string AddressDelivery { get; set; }
        public ICollection<ProductRating> Ratings { get; set; }

        public User()
        {
            Ratings = new List<ProductRating>();
        }
    }
}