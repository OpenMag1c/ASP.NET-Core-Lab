using System.Collections.Generic;
using System.Threading.Tasks;
using Business.DTO;

namespace Business.Interfaces
{
    public interface IUserService
    {
        List<string> GetUserLogins();
        Task<UserDTO> UpdateUserAsync(string userId, UserDTO userDto);
        Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
    }
}