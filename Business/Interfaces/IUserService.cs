using System.Collections.Generic;
using System.Threading.Tasks;
using Business.DTO;

namespace Business.Interfaces
{
    public interface IUserService
    {
        public Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
        public Task<UserDTO> GetProfileInfoAsync(string userId);
        public Task<List<string>> GetUserLoginsAsync();
        public Task<UserDTO> UpdateUserAsync(string userId, UserDTO userDto);
    }
}