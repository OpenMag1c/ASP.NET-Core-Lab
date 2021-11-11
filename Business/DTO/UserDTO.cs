using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Business.DTO
{
    public class UserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string AddressDelivery { get; set; }
        public int Age { get; set; }
        public  string PhoneNumber { get; set; }
        public string UserName { get; set; }
    }
}