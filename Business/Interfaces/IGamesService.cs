using System.Collections.Generic;
using Business.DTO;

namespace Business.Interfaces
{
    public interface IGamesService
    {
        public IEnumerable<PlatformDTO> GetTopThreePlatforms();

        public IEnumerable<ProductDTO> SearchProductsByTerm(string term, int limit, int offset);
    }
}