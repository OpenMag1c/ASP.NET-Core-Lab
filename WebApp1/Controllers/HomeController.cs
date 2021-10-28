using Business.Interfaces;
using DAL.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebApp1.Controllers
{
    [Route("api/HomeController/")]
    public class HomeController : ControllerBase
    {
        private IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("AddUser")]
        public string AddUser()
        {
            var user = new UserDTO();
            _userService.AddUser(user);
            return "Successful";
        }

        [HttpGet("GetInfo")]
        public string GetInfo()
        {
            return _userService.GetAllUsers().ToString();
        }
    }  
}