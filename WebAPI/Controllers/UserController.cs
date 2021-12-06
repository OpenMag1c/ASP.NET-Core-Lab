using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Business.DTO;
using Business.Helper;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(ILogger logger, IUserService userService) : base(logger)
        {
            _userService = userService;
        }

        /// <summary>
        /// Update user profile by model
        /// </summary>
        /// <response code="200">Profile updated</response>
        /// <response code="400">Wrong params format</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Oops!</response>
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<UserDTO>> UpdateProfile([FromBody] UserDTO userDto)
        {
            var userId = UserHelpers.GetUserIdByClaim(User.Claims);
            var updatedUserDto = await _userService.UpdateUserAsync(userId, userDto); 

            return updatedUserDto;
        }
        
        /// <summary>
        /// Update user password
        /// </summary>
        /// <response code="200">Profile updated</response>
        /// <response code="204">Profile password updated</response>
        /// <response code="400">Wrong params format</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Oops!</response>
        [HttpPatch("password")]
        [Authorize]
        public async Task<IActionResult> ChangeProfilePassword([Required] string oldPassword, [Required] string newPassword)
        {
            var userId = UserHelpers.GetUserIdByClaim(User.Claims);
            await _userService.ChangePasswordAsync(userId, oldPassword, newPassword);

            return NoContent();
        }

        /// <summary>
        /// Represent user info
        /// </summary>
        /// <response code="200">All OK</response>
        /// <response code="400">Wrong params format</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Oops!</response>
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
