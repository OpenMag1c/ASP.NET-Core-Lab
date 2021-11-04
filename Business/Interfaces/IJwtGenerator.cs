using DAL.Models;

namespace Business.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(User user);
    }
}