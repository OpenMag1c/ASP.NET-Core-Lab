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
        public string Name { get; set; }

        /// <summary> 
        /// Product platform name
        /// </summary> 
        /// <example> 
        /// PC; 
        /// Mobile; 
        /// Console; 
        /// </example>
        public Platforms Platform { get; set; }

        /// <summary> 
        /// Product creating data
        /// </summary> 
        /// <example> 
        /// 2018
        /// </example>
        public int DateCreated { get; set; }

        /// <summary> 
        /// Total rating
        /// </summary> 
        /// <example> 
        /// 77
        /// </example>
        public int TotalRating { get; set; }

        /// <summary> 
        /// Genre
        /// </summary> 
        /// <example> 
        /// RPG/Action
        /// </example>
        public string Genre { get; set; }

        /// <summary> 
        /// Game rating
        /// </summary> 
        /// <example> 
        /// 7
        /// </example>
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
        public double Price { get; set; }

        /// <summary> 
        /// Number of games in stock 
        /// </summary> 
        /// <example> 
        /// 999
        /// </example>
        public int Count { get; set; }
    }
}
