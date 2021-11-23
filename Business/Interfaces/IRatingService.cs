using System.Threading.Tasks;
using Business.DTO;

namespace Business.Interfaces
{
    public interface IRatingService
    {
        public ProductOutputDTO EditProductRating(string userId, int productId, int rating);
        public void DeleteRating(string userId, int productId);
    }
}