using System;
using System.Net;
using System.Threading.Tasks;
using Business.DTO;
using Business.ExceptionMiddleware;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
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
        /// <response code="401">Unauthorized</response>
        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] UserCredentialsDTO userCredentialsDto)
        {
            var jwt = await _authenticationService.SignInAsync(userCredentialsDto);

            return jwt is null ? Unauthorized() : Ok(jwt);
        }

        /// <summary>
        /// Registration by email and password
        /// </summary>
        /// <response code="201">Registration completed</response>
        /// <response code="400">Registration not completed</response>
        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] UserCredentialsDTO userCredentialsDto)
        {
            var result = await _authenticationService.SignUpAsync(userCredentialsDto);

            return result ? Created(new Uri(Request.GetDisplayUrl()), userCredentialsDto) : BadRequest(Messages.NotCompleted);
        }

        /// <summary>
        /// Email confirmation
        /// </summary>
        /// <response code="204">Email confirmed</response>
        /// <response code="400">Unsuccessful email confirmation</response>
        [HttpGet("email-confirmation")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(int id, string token)
        {
            var result = await _authenticationService.ConfirmEmailAsync(id, token);

            return result ? NoContent() : BadRequest(Messages.EmailUnconfirmed);
        }
    }
}