using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Business.DTO;
using DAL.Models;

namespace Business.Interfaces
{
    public interface IUserService
    {
        UserCredentialsDTO GetUserById(int? id);
        void Register(UserCredentialsDTO user);
        List<string> GetUserLogins();
    }
}