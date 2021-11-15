using System.Threading.Tasks;
using Business.DTO;
using Business.Interfaces;
using Business.Services;
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
            var userDto = await _authenticationService.SignUpAsync(userCredentialsDto);
            if (userDto is null)
            {
                return BadRequest();
            }

            var callbackUrl = Url.Action("ConfirmEmail", "Auth", new { id = userDto.Id, token = userDto.Token }, protocol: HttpContext.Request.Scheme);
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(userDto.Email, "Confirm your account",
                $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

            return Ok();
        }

        [HttpGet("email-confirmation")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(int id, string token)
            {
            var result = await _authenticationService.ConfirmEmailAsync(id, token);

            return result ? NoContent() : BadRequest("Email is unconfirmed.");
        }
    }
}