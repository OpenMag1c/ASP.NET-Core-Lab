using Business.DTO;
using System;
using System.Threading.Tasks;


namespace Business.Interfaces
{
    public interface IAuthService
    {
        Task<String> SingInAsync(UserCredentialsDTO userCredentialsDto);
    }
}
