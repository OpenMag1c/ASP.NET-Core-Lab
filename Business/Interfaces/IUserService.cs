using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Business.DTO;
using DAL.Models;

namespace Business.Interfaces
{
    public interface IUserService
    {
        UserDTO GetUserById(int? id);
        void Register(UserDTO user);
        List<string> GetUserLogins();
    }
}