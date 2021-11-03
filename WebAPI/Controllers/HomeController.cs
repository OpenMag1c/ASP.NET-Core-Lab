using System.Text;
using Business.DTO;
using Serilog;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/HomeController/")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public HomeController(IUserService userService, ILogger logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("PostUser")]
        public void AddUser([FromBody] string name)
        {
            var user = new UserDto
            {
                Name = name,
            };

            _userService.AddUser(user);
        }

        [HttpGet("GetInfo")]
        public string GetInfo()
        {
            var allUsers = _userService.GetAllUsers();
            var users = new StringBuilder();
            foreach (var user in allUsers)
            {
                users.Append($"{user.Id}. {user.Name}\n");
            }

            _logger.ForContext<HomeController>().Information("request: GetInfo");
            return users.ToString();
        }
    }  
}