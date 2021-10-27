using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp1.Controllers
{
    public class HomeController : ControllerBase
    {

        public HomeController()
        {
        }

        [HttpGet]
        [Route("Controllers/HomeController")]
        public string GetInfo()
        {
            return "Hello World!";
        }
    }  
}
