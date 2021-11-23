using System.Threading.Tasks;
using Business.DTO;

namespace Business.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> ConfirmEmailAsync(int id, string token);
        Task<string> SignInAsync(UserCredentialsDTO userCredentialsDto);

        Task<bool> SignUpAsync(UserCredentialsDTO userCredentialsDto);
    }
}