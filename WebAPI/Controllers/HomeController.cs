﻿using System.Text;
using Serilog;
using Business.Interfaces;
using DAL.Enum;
using DAL.Models;
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