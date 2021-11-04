using DAL.Models;
using MediatR;

namespace Business.Authentication.Registration
{
	public class RegistrationCommand : IRequest<User>
	{
        public string UserName { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }
	}
}