using System.ComponentModel.DataAnnotations;
using DAL.Enum;

namespace Business.DTO
{
    public class ProductOutputDTO
    {
        /// <summary> 
        /// Product name 
        /// </summary> 
        /// <example>"My Best Game"</example>
        public string Name { get; set; }

        /// <summary> 
        /// Product platform name
        /// </summary> 
        /// <example> 
        /// PC; 
        /// Mobile; 
        /// Console; 
        /// </example>
        public string Platform { get; set; }

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
        public string Rating { get; set; }

        /// <summary> 
        /// Image-logo
        /// </summary> 
        /// <example> 
        /// url
        /// </example>
        public string Logo { get; set; }

        /// <summary> 
        /// Image-background
        /// </summary> 
        /// <example> 
        /// url
        /// </example>
        public string Background { get; set; }

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
