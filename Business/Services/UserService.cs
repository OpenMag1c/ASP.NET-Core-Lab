using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business.DTO;
using Business.ExceptionMiddleware;
using Business.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public List<string> GetUserLogins()
        {
            var result = new List<string>();
            foreach (var user in _userManager.Users)
            {
                result.Add(user.UserName);
            }

            return result;
        }

        public async Task<UserDTO> UpdateUserAsync(string userId, UserDTO userDto)
        {
            var oldUser = await _userManager.FindByIdAsync(userId);
            var newUser = _mapper.Map(userDto, oldUser);
            var result = await _userManager.UpdateAsync(newUser);
            if (!result.Succeeded)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest, Messages.NotCompleted);
            }

            return _mapper.Map<UserDTO>(newUser);
        }

        public async Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var isRightPassword = await _userManager.CheckPasswordAsync(user, oldPassword);
            if (user is null || !isRightPassword)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, Messages.WrongPassword);
            }

            await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            return true;
        }
        public async Task<UserDTO> GetProfileInfoAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new HttpStatusException(HttpStatusCode.NotFound, Messages.UserNotFound);
            }

            return _mapper.Map<UserDTO>(user);
        }
    }
}