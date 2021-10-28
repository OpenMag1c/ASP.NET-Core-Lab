using System.Collections.Generic;
using Business.DTO;

namespace Business.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAllUsers();
        UserDto GetUserById(int? id);
        void AddUser(UserDto userDto);
    }
}