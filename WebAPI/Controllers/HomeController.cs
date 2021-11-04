using System.Text;
using System.Threading.Tasks;
using Business.DTO;
using Serilog;
using Business.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/HomeController/")]
    public class HomeController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public HomeController(IUserService userService, ILogger logger)
        {
            _userService = userService;
            _logger = logger;
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