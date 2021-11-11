using Microsoft.AspNetCore.Identity;

namespace DAL.Models
{
    public class User : IdentityUser<int>
    {
        public int Age { get; set; }
        public string AddressDelivery { get; set; }
    }
}