using System.ComponentModel.DataAnnotations;
using DAL.Enum;

namespace Business.DTO
{
    public class ProductDTO
    {
        /// <summary> 
        /// Product name 
        /// </summary> 
        /// <example>"My Best Game"</example>
        [Required]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Name must contain only letters")]
        public string Name { get; set; }

        /// <summary> 
        /// Product platform name
        /// </summary> 
        /// <example> 
        /// PC; 
        /// Mobile; 
        /// Console; 
        /// </example>
        [Required]
        public string Platform { get; set; }
    }
}
