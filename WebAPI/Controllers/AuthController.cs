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
        private readonly IAuthenticationService _authenticationService;

        public AuthController(ILogger logger, IAuthenticationService authenticationService) : base(logger)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Authorization by email and password
        /// </summary>
        /// <response code="200">Authorized</response>
        /// <response code="400">Wrong params format</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Oops!</response>
        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] UserCredentialsDTO userCredentialsDto)
        {
            var jwt = await _authenticationService.SignInAsync(userCredentialsDto);

            return Ok(jwt);
        }

        /// <summary>
        /// Registration by email and password
        /// </summary>
        /// <response code="200">Registration completed</response>
        /// <response code="400">Wrong params format</response>
        /// <response code="500">Oops!</response>
        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] UserCredentialsDTO userCredentialsDto)
        {
            await _authenticationService.SignUpAsync(userCredentialsDto);

            return Ok();
        }

        /// <summary>
        /// Email confirmation
        /// </summary>
        /// <response code="200">Registration completed</response>
        /// <response code="204">Email confirmed</response>
        /// <response code="400">Email unconfirmed</response>
        /// <response code="500">Oops!</response>
        [HttpGet("email-confirmation")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(int id, string token)
        {
            await _authenticationService.ConfirmEmailAsync(id, token);

            return NoContent();
        }
    }
}