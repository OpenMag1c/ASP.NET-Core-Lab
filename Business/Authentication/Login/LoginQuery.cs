using MediatR;

namespace Business.Authentication.Login
{
    public class LoginQuery : IRequest<DAL.Models.User>
	{
		public string Email { get; set; }

		public string Password { get; set; }
	}
}
