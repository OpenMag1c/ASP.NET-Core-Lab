using Business.Interfaces;
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

        [HttpGet("GetInfo")]
        public string GetInfo()
        {
            return _userService.GetUserByName("");
        }
    }  
}
