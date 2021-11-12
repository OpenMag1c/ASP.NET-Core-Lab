using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Business.DTO;
using Business.Helper;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using WebAPI.Pages;

namespace WebAPI.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(ILogger logger, IUserService userService) : base(logger)
        {
            _userService = userService;
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<UserDTO>> UpdateProfile([FromBody] UserDTO userDto)
        {
            var userId = UserHelpers.GetUserIdByClaim(User.Claims);
            var updatedUserDto = await _userService.UpdateUserAsync(userId, userDto); 

            return updatedUserDto;
        }

        [HttpPatch("password")]
        [Authorize]
        public async Task<IActionResult> ChangeProfilePassword(string oldPassword, string newPassword)
        {
            var userId = UserHelpers.GetUserIdByClaim(User.Claims);
            var result = await _userService.ChangePasswordAsync(userId, oldPassword, newPassword);

            return result ? NoContent() : BadRequest(Messages.NotCompleted);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetProfileInfo()
        {
            var userId = UserHelpers.GetUserIdByClaim(User.Claims);
            var userDto = await _userService.GetProfileInfoAsync(userId);

            return userDto;
        }
    }
}
