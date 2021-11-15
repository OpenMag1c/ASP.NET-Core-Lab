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

            return (isRightPassword && await _userManager.IsEmailConfirmedAsync(user)) ? await _jwtGenerator.GenerateJwtTokenAsync(user) : null;
        }

        public async Task<ConfirmEmailDTO> SignUpAsync(UserCredentialsDTO userCredentialsDto)
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
                return new ConfirmEmailDTO()
                {
                    Email = user.Email,
                    Id = user.Id,
                    Token = await _userManager.GenerateEmailConfirmationTokenAsync(user)
                };
            }

            return null;
        }

        public async Task<bool> ConfirmEmailAsync(int id, string token)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
            {
                return false;
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;
        }
    }
}