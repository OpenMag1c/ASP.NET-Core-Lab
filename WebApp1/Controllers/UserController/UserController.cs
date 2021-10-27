using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp1.Controllers.UserController
{
    public class UserController : Controller
    {
        private IUserService _userService;
        public UserController(IUserService service) { 
            
        }
        public IActionResult Index()
        {
            var info = _userService.GetInfo()
            return View();
        }
    }
}
