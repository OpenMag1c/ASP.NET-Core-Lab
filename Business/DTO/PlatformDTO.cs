using System.Collections.Generic;

namespace Business.DTO
{
    public class PlatformDTO
    {
        /// <summary> 
        /// Product platform Name
        /// </summary> 
        /// <example> 
        /// PC; 
        /// Mobile; 
        /// Console; 
        /// </example>
        public string Name { get; set; }

        /// <summary> 
        /// Product games
        /// </summary> 
        /// <example> 
        /// Horizon;
        /// Counter Strike
        /// </example> 
        public IEnumerable<string> Games { get; set; }
    }
}