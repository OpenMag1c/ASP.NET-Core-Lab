using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Business.DTO;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebAPI.Controllers
{
    public class AuthController : BaseController
    {
        private IAuthenticationService _authenticationService;

        public AuthController(ILogger logger, IAuthenticationService authenticationService) : base(logger)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] UserCredentialsDTO userCredentialsDto)
        {
            var jwt = await _authenticationService.SignInAsync(userCredentialsDto);

            return jwt is null ? Unauthorized("Wrong email or password") : Ok(jwt);
        }

        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] UserCredentialsDTO userCredentialsDto)
        {
            var result = await _authenticationService.SignUpAsync(userCredentialsDto);

            return !result ? BadRequest() : Ok();
        }

        [HttpGet("email-confirmation")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromBody] int id, [FromBody] string jwt)
        {
            var result = await _authenticationService.ConfirmEmailAsync(id, jwt);

            return result ? NoContent() : BadRequest("Wrong token or id");
        }
    }
}