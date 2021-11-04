using System;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Business.Exception;
using Business.Interfaces;
using DAL.Database;
using DAL.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Authentication.Registration
{
	public class RegistrationHandler : IRequestHandler<RegistrationCommand, User>
	{
		private readonly UserManager<User> _userManager;
		private readonly IJwtGenerator _jwtGenerator;
        private readonly ApplicationDbContext _context;

		public RegistrationHandler(ApplicationDbContext context, UserManager<User> userManager, IJwtGenerator jwtGenerator)
		{
			_context = context;
			_userManager = userManager;
			_jwtGenerator = jwtGenerator;
		}

		public async Task<User> Handle(RegistrationCommand request, CancellationToken cancellationToken)
		{
			if (await _context.Users.Where(x => x.Email == request.Email).AnyAsync())
			{
				throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exist" });
			}

			if (await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync())
			{
				throw new RestException(HttpStatusCode.BadRequest, new { UserName = "UserName already exist" });
			}

			var user = new User
							{
								Email = request.Email,
								UserName = request.UserName
							};

			var result = await _userManager.CreateAsync(user, request.Password);

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