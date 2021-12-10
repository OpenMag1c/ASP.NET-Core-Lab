using System.Threading.Tasks;
using Business.DTO;

namespace Business.Interfaces
{
    public interface IRatingService
    {
        public Task<ProductOutputDTO> EditProductRatingAsync(string userId, int productId, int rating);
        public Task<bool> DeleteRatingAsync(string userId, int productId);
    }
}