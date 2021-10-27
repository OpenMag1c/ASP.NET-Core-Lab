using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp1.Controllers
{
    public class HomeController1 : ControllerBase
    {

        public HomeController1()
        {
        }

        [HttpGet]
        [Route("GetInfo")]
        public string GetInfo()
        {
            return "{\"info\":\"Hello World\"}";
        }
    }  
}
