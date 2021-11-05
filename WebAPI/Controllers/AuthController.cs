using Business.DTO;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService, ILogger logger) {
            _authService = authService;
        }

        public async Task<ActionResult> SingInAsync(UserCredentialsDTO userCredDTO)
        {
            var jwtToken =  await _authService.SingInAsync(userCredDTO);
            if (jwtToken == null)
            {
                return Unauthorized("Wrong email or password");
            }
            else 
            {
                return Ok(jwtToken);
            }

        }
    }
}
