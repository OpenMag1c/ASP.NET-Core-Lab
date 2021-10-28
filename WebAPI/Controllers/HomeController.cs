using System.Text;
using Business.DTO;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/HomeController/")]
    public class HomeController : ControllerBase
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("PostUser")]
        public void AddUser([FromBody] string name)
        {
            var user = new UserDto
            {
                Name = name,
                Age = 0,
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
                users.Append($"{user.Id}. {user.Name}, {user.Age}\n");
            }

            return users.ToString();
        }
    }  
}