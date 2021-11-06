using System.Threading.Tasks;
using AutoMapper;
using Business.DTO;
using Business.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;

        private readonly UserManager<User> _userManager;

        private readonly IJwtGenerator _jwtGenerator;

        public AuthenticationService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
        }
        public async Task<string> SignInAsync(UserCredentialsDTO userCredentialsDto)
        {
            var user = await _userManager.FindByEmailAsync(userCredentialsDto.Email);
            if (user == null)
            {
                return null;
            }

            var isRightPassword = await _userManager.CheckPasswordAsync(user, userCredentialsDto.Password);

            return isRightPassword ? _jwtGenerator.CreateToken(user) : null;
        }

        public async Task<bool> SignUpAsync(UserCredentialsDTO userCredentialsDto)
        {
            var user = _mapper.Map<User>(userCredentialsDto);
            if (user.UserName is null)
            {
                user.UserName = user.Email;
            }

            var result = await _userManager.CreateAsync(user, user.PasswordHash);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
            }

            return result.Succeeded;
        }

        public async Task<bool> ConfirmEmailAsync(int id, string jwt)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user != null && jwt == _jwtGenerator.CreateToken(user))
            {
                user.EmailConfirmed = true;
                return true;
            }

            return false;
        }
    }
}