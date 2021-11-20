using System.Collections.Generic;
using System.Threading.Tasks;
using Business.DTO;

namespace Business.Interfaces
{
    public interface IUserService
    {
        Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
        Task<UserDTO> GetProfileInfoAsync(string userId);
        List<string> GetUserLogins();
        Task<UserDTO> UpdateUserAsync(string userId, UserDTO userDto);
    }
}