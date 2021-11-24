using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.DTO;
using Business.ExceptionMiddleware;
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
        private readonly EmailService _emailService;

        public AuthenticationService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator, EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
            _emailService = emailService;
        }

        public async Task<string> SignInAsync(UserCredentialsDTO userCredentialsDto)
        {
            var user = await _userManager.FindByEmailAsync(userCredentialsDto.Email);
            if (user is null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, Messages.WrongEmail);
            }

            var isRightPassword = await _userManager.CheckPasswordAsync(user, userCredentialsDto.Password);
            var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!isRightPassword && !isEmailConfirmed)
            {
                throw new HttpStatusException(HttpStatusCode.Unauthorized, Messages.WrongPasswordOrEmail);
            }

            return await _jwtGenerator.GenerateJwtTokenAsync(user);
        }

        public async Task<bool> SignUpAsync(UserCredentialsDTO userCredentialsDto)
        {
            var user = _mapper.Map<User>(userCredentialsDto);
            if (user is null)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, Messages.NotCompleted);
            }

            var result = await _userManager.CreateAsync(user, user.PasswordHash);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var tokenBytes = Encoding.UTF8.GetBytes(token);
                var tokenEncoded = WebEncoders.Base64UrlEncode(tokenBytes);
                await _emailService.SendEmailAsync(user.Email, "Confirm your account",
                    $"https://localhost:44328/api/Auth/email-confirmation?id={user.Id}&token={tokenEncoded}"); 
                return true;
            }

            throw new HttpStatusException(HttpStatusCode.BadRequest, Messages.NotCompleted);
        }

        public async Task<bool> ConfirmEmailAsync(int id, string token)
        {
            var tokenDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var tokenDecodedString = Encoding.UTF8.GetString(tokenDecodedBytes);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, Messages.UserNotFound);
            }

            var result = await _userManager.ConfirmEmailAsync(user, tokenDecodedString);
            return result.Succeeded;
        }
    }
}