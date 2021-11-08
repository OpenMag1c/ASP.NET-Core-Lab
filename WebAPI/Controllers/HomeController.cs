using System.Security;
using System.Text;
using Serilog;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class HomeController : BaseController
    {
        private IUserService _userService;

        public HomeController(IUserService userService, ILogger logger) : base(logger)
        {
            _userService = userService;
        }

        [HttpGet("GetInfo")]
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