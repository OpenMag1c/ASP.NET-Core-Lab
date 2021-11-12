using System.Collections.Generic;

namespace Business.DTO
{
    public class PlatformDTO
    {
        public string Name { get; set; }
        public IEnumerable<string> Games { get; set; }
    }
}