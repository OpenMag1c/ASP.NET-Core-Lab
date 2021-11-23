using System.Text;
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
        /// Get all user logins
        /// </summary>
        /// <response code="200">All OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Access denied</response>
        /// <response code="500">Oops!</response>
        [HttpGet("GetInfo")]
        [Authorize(Roles = Roles.Admin)]
        public string GetInfo()
        {
            var str = new StringBuilder();
            foreach (var login in _userService.GetUserLogins())
            {
                str.Append(login);
                str.Append("\n");
            }
            _logger.ForContext<HomeController>().Information("request: GetInfo");

            return str.ToString();
        }
    }  
}