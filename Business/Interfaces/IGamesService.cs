using System.Collections.Generic;
using System.Threading.Tasks;
using Business.DTO;

namespace Business.Interfaces
{
    public interface IGamesService
    {
        public IEnumerable<PlatformDTO> GetTopThreePlatforms();
        public IEnumerable<ProductOutputDTO> SearchProductsByTerm(string term, int limit, int offset);
        public ProductOutputDTO FindProductById(int id);
        public Task<ProductOutputDTO> AddProductAsync(ProductInputDTO productInputDto);
        public Task<ProductOutputDTO> UpdateProductAsync(ProductInputDTO productInputDto);
        public void DeleteProductAsync(int id);
    }
}