using System.Collections.Generic;
using DAL.DTO;
using DAL.Models;

namespace Business.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetAllUsers();
        UserDTO GetUserById(int? id);
        void AddUser(UserDTO userDto);
        void Dispose();
    }
}