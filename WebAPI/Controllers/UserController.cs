using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Business.DTO;
using Business.Helper;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Serilog;

namespace WebAPI.Controllers
{
    public class UserController : BaseController
    {
        private IUserService _userService;
        public UserController(ILogger logger, IUserService userService) : base(logger)
        {
            _userService = userService;
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UserDTO userDto)
        {
            var result = await _userService.UpdateUserAsync(UserHelpers.GetUserIdByClaim(User.Claims), userDto);
            return Ok();
        }

        [HttpPatch("password")]
        [Authorize]
        public async Task<IActionResult> ChangeProfilePassword(string oldPassword, string newPassword)
        {
            var result = await _userService.ChangePasswordAsync(UserHelpers.GetUserIdByClaim(User.Claims), oldPassword, newPassword);
            return Ok();
        }
    }
}
