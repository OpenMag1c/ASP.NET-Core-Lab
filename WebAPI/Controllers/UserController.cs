using System.Threading.Tasks;
using Business.Authentication.Login;
using Business.Authentication.Registration;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    public class UserController : BaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<User>> LoginAsync(LoginQuery query)
        {
            return await Mediator.Send(query);
        }

		[HttpPost("registration")]
		public async Task<ActionResult<User>> RegistrationAsync(RegistrationCommand command)
		{
			return await Mediator.Send(command);
		}
    }
}
