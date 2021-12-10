using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class Pagination
    {
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers!")]
        [Range(1,100)]
        public int PageNumber { get; set; } = 1;

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers!")]
        [Range(1, 100)]
        public int PageSize { get; set; }
    }
}