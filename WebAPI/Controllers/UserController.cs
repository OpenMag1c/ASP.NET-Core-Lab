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
        public async Task<string> UpdateProfile([FromBody] UserDTO userDto)
        {
            var updatedUserDto = await _userService.UpdateUserAsync(UserHelpers.GetUserIdByClaim(User.Claims), userDto); 

            return _userService.GetUserDtoStr(updatedUserDto);
        }

        [HttpPatch("password")]
        [Authorize]
        public async Task<IActionResult> ChangeProfilePassword(string oldPassword, string newPassword)
        {
            var result = await _userService.ChangePasswordAsync(UserHelpers.GetUserIdByClaim(User.Claims), oldPassword, newPassword);

            return result ? NoContent() : BadRequest();
        }

        [HttpGet]
        [Authorize]
        public async Task<string> GetProfileInfo()
        {
            var userDto = await _userService.GetProfileInfoAsync(UserHelpers.GetUserIdByClaim(User.Claims));

            return _userService.GetUserDtoStr(userDto);
        }
    }
}
