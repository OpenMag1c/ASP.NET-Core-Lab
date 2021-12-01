using System;
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
using Microsoft.Extensions.Caching.Memory;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IMemoryCache _cache;

        public UserService(IMapper mapper, UserManager<User> userManager, IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _userManager = userManager;
            _cache = memoryCache;
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

            _cache.Remove(userId);
            var newUserDto = _mapper.Map<UserDTO>(newUser);

            return newUserDto;
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
            _cache.Remove(userId);

            return true;
        }

        public async Task<UserDTO> GetProfileInfoAsync(string userId)
        {
            if (!_cache.TryGetValue(userId, out User user))
            {
                user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    _cache.Set(user.Id, user,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
                else
                {
                    throw new HttpStatusException(HttpStatusCode.NotFound, Messages.UserNotFound);
                }
            }

            var userDto = _mapper.Map<UserDTO>(user);

            return userDto;
        }
    }
}