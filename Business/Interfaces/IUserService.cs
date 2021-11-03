using System.Collections.Generic;
using System.Security.Claims;
using Business.DTO;

namespace Business.Interfaces
{
    public interface IUserService
    {
        ClaimsIdentity GetIdentity(string username, string password);
    }
}