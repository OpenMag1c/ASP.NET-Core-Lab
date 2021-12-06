using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace WebAPI.Tests.Extensions
{
    public static class ControllerTestExtensions
    {
        public static TController WithTestUser<TController>(this TController controller)
            where TController : ControllerBase
        {
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                    {
                        new Claim("nameid", "1"),
                        new Claim(ClaimTypes.Role, "user")
                    }))
                }
            };

            return controller;
        }
    }
}
