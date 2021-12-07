using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.DTO;
using Business.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly SendingEmail _sendingEmail;

        public AuthenticationService(IMapper mapper, UserManager<User> userManager, IJwtGenerator jwtGenerator, SendingEmail sendingEmail)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
            _sendingEmail = sendingEmail;
        }

        public async Task<string> SignInAsync(UserCredentialsDTO userCredentialsDto)
        {
            var user = await _userManager.FindByEmailAsync(userCredentialsDto.Email);
            if (user is null)
            {
                return await Task.FromResult<string>(null);
            }

            var isRightPassword = await _userManager.CheckPasswordAsync(user, userCredentialsDto.Password);
            var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!isRightPassword || !isEmailConfirmed)
            {
                return await Task.FromResult<string>(null);
            }

            var token = await _jwtGenerator.GenerateJwtTokenAsync(user);

            return token;
        }

        public async Task<bool> SignUpAsync(UserCredentialsDTO userCredentialsDto)
        {
            var user = _mapper.Map<User>(userCredentialsDto);
            var result = await _userManager.CreateAsync(user, user.PasswordHash);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var tokenBytes = Encoding.UTF8.GetBytes(token);
                var tokenEncoded = WebEncoders.Base64UrlEncode(tokenBytes);
                await _sendingEmail.SendEmailAsync(user.Email, "Confirm your account",
                    $"https://localhost:44328/api/Auth/email-confirmation?id={user.Id}&token={tokenEncoded}"); 

                return true;
            }

            return false;
        }

        public async Task<bool> ConfirmEmailAsync(int id, string token)
        {
            var tokenDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var tokenDecodedString = Encoding.UTF8.GetString(tokenDecodedBytes);

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is null)
            {
                return false;
            }

            var result = await _userManager.ConfirmEmailAsync(user, tokenDecodedString);

            return result.Succeeded;
        }
    }
}