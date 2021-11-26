using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Business.DTO;
using Business.ExceptionMiddleware;
using Business.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<string>> GetUserLoginsAsync()
        {
            var result = await _userManager.Users.Select(user => user.UserName).ToListAsync();

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