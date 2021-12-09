using System.ComponentModel.DataAnnotations;

namespace Business.DTO
{
    public class UserDTO
    {
        /// <summary> 
        /// User email 
        /// </summary> 
        /// <example>admintop_mail@gmail.com</example>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary> 
        /// User delivery address 
        /// </summary> 
        /// <example>Minsk, BNTU 1/1</example>
        [Required]
        [StringLength(25)]
        public string AddressDelivery { get; set; }

        /// <summary> 
        /// User age 
        /// </summary> 
        /// <example>19</example>
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers!")]
        public int Age { get; set; }

        /// <summary> 
        /// User phone number
        /// </summary> 
        /// <example>37529XXXXXXX</example>
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers!")]
        public string PhoneNumber { get; set; }

        /// <summary> 
        /// User name
        /// </summary> 
        /// <example>"OpenMXGIC"</example>
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.-]*$", ErrorMessage = "Without whitespaces!")]
        public string UserName { get; set; }
    }
}