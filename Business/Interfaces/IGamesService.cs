using System.Collections.Generic;
using System.Threading.Tasks;
using Business.DTO;

namespace Business.Interfaces
{
    public interface IGamesService
    {
        public IEnumerable<PlatformDTO> GetTopThreePlatforms();
        public IEnumerable<ProductDTO> SearchProductsByTerm(string term, int limit, int offset);
        public ProductDTO FindProductById(int id);
        public Task<ProductDTO> AddNewProductAsync(ProductDTO productDto);
        public Task<ProductDTO> UpdateProductAsync(ProductDTO productDto);
        public void DeleteProductAsync(int id);
    }
}