using System.Collections.Generic;
using System.Threading.Tasks;
using Business.DTO;
using Business.Helper;
using Business.Models;

namespace Business.Interfaces
{
    public interface IGamesService
    {
        public Task<ProductOutputDTO> AddProductAsync(ProductInputDTO productInputDto);
        public Task DeleteRatingAsync(string userId, int productId);
        public void DeleteProduct(int id);
        public Task<ProductOutputDTO> EditProductRatingAsync(string userId, string productName, int rating);
        public ProductOutputDTO FindProductById(int id);
        public IEnumerable<ProductOutputDTO> GetProducts(Pagination pagination, ProductFilters filters);
        public IEnumerable<PlatformDTO> GetTopThreePlatforms();
        public IEnumerable<ProductOutputDTO> SearchProductsByTerm(string term, int limit, int offset);
        public Task<ProductOutputDTO> UpdateProductAsync(ProductInputDTO productInputDto);
    }
}