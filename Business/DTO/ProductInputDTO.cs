using System.ComponentModel.DataAnnotations;
using DAL.Enum;
using Microsoft.AspNetCore.Http;

namespace Business.DTO
{
    public class ProductInputDTO
    {
        /// <summary> 
        /// Product name 
        /// </summary> 
        /// <example>"My Best Game"</example>
        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        /// <summary> 
        /// Product platform name
        /// </summary> 
        /// <example> 
        /// PC; 
        /// Mobile; 
        /// Console; 
        /// </example>
        [EnumDataType(typeof(Platforms))]
        public Platforms Platform { get; set; }

        /// <summary> 
        /// Product creating data
        /// </summary> 
        /// <example> 
        /// 2018
        /// </example>
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers!")]
        public int DateCreated { get; set; }

        /// <summary> 
        /// Genre
        /// </summary> 
        /// <example> 
        /// RPG/Action
        /// </example>
        [EnumDataType(typeof(Genres))]
        public Genres Genre { get; set; }

        /// <summary> 
        /// Game rating
        /// </summary> 
        /// <example> 
        /// 7
        /// </example>
        [EnumDataType(typeof(Ratings))]
        public Ratings Rating { get; set; }

        /// <summary> 
        /// Image-logo
        /// </summary> 
        /// <example> 
        /// D:/image.jpg
        /// </example>
        public IFormFile Logo { get; set; }

        /// <summary> 
        /// Image-background
        /// </summary> 
        /// <example> 
        /// D:/image.jpg
        /// </example>
        public IFormFile Background { get; set; }

        /// <summary> 
        /// Game price
        /// </summary> 
        /// <example> 
        /// 4.99
        /// </example>
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers!")]
        public double Price { get; set; }

        /// <summary> 
        /// Number of games in stock 
        /// </summary> 
        /// <example> 
        /// 999
        /// </example>
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers!")]
        public int Count { get; set; }
    }
}
