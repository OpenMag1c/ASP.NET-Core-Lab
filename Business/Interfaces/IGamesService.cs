using System.Collections.Generic;
using Business.DTO;

namespace Business.Interfaces
{
    public interface IGamesService
    {
        public IEnumerable<PlatformDTO> GetTopThreePlatforms();
    }
}