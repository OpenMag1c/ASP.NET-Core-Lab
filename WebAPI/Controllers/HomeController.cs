using System.Text;
using System.Threading.Tasks;
using Serilog;
using Business.Interfaces;
using DAL.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService, ILogger logger) : base(logger)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all user logins, only for admin
        /// </summary>
        /// <response code="200">All OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Access denied</response>
        [HttpGet("GetInfo")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<string>> GetInfo()
        {
            var str = new StringBuilder();
            var logins = await _userService.GetUserLoginsAsync();
            foreach (var login in logins)
            {
                str.Append(login);
                str.Append("\n");
            }

            return str.ToString();
        }
    }  
}