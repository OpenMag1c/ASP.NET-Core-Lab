using System.Collections.Generic;
using System.Threading.Tasks;
using Business.DTO;
using DAL.FilterModels;

namespace Business.Interfaces
{
    public interface IGamesService
    {
        public Task<ProductOutputDTO> AddProductAsync(ProductInputDTO productInputDto);
        public Task<bool> DeleteProductAsync(int id);
        public Task<ProductOutputDTO> FindProductByIdAsync(int id);
        public Task<IEnumerable<ProductOutputDTO>> GetProductsAsync(Pagination pagination, ProductFilters filters);
        public Task<IEnumerable<PlatformDTO>> GetTopThreePlatformsAsync();
        public Task<IEnumerable<ProductOutputDTO>> SearchProductsByTermAsync(string term, int limit, int offset);
        public Task<ProductOutputDTO> UpdateProductAsync(ProductInputDTO productInputDto);
    }
}