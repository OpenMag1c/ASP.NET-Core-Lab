using System.Net;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Business.Exception;
using Business.Interfaces;
using DAL.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Business.Authentication.Login
{
    public class LoginHandler : IRequestHandler<LoginQuery, User>
	{
		private readonly UserManager<User> _userManager;

		private readonly SignInManager<User> _signInManager;

		private readonly IJwtGenerator _jwtGenerator;

		public LoginHandler(UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_jwtGenerator = jwtGenerator;
		}

		public async Task<User> Handle(LoginQuery request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByEmailAsync(request.Email);
			if (user == null)
			{
				throw new RestException(HttpStatusCode.Unauthorized);
			}

			var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

			if (result.Succeeded)
			{
				return new User
							{
								Token = _jwtGenerator.CreateToken(user),
								UserName = user.UserName,
							};
			}

			throw new RestException(HttpStatusCode.Unauthorized);
		}
	}
}
