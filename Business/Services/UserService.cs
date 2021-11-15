using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.DTO;
using Business.Interfaces;
using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Identity;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repo;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(IRepository<User> repo, IMapper mapper, UserManager<User> userManager)
        {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
        }

        public List<string> GetUserLogins()
        {
            var result = new List<string>();
            foreach (var user in _repo.GetAllUsers())
            {
                result.Add(user.UserName);
            }

            return result;
        }

        public async Task<UserDTO> UpdateUserAsync(string userId, UserDTO userDto)
        {
            var oldUser = await _userManager.FindByIdAsync(userId);
            if (oldUser is null)
            {
                return null;
            }

            var newUser = _mapper.Map(userDto, oldUser);
            var result = await _userManager.UpdateAsync(newUser);
            if (!result.Succeeded)
            {
                return null;
            }

            return _mapper.Map<UserDTO>(newUser);
        }

        public async Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var isRightPassword = await _userManager.CheckPasswordAsync(user, oldPassword);
            if (user is null || !isRightPassword)
            {
                return false;
            }

            await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            return true;
        }
        public async Task<UserDTO> GetProfileInfoAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return null;
            }

            return _mapper.Map<UserDTO>(user);
        }

        public string GetUserDtoStr(UserDTO userDto)
        {
            var userDtoStr = new StringBuilder();
            userDtoStr.Append($"Username: {userDto.UserName}")
                .Append(Environment.NewLine)
                .Append($"Email: {userDto.Email}")
                .Append(Environment.NewLine)
                .Append($"Age: {userDto.Age}")
                .Append(Environment.NewLine)
                .Append($"AddressDelivery: {userDto.AddressDelivery}")
                .Append(Environment.NewLine)
                .Append($"PhoneNumber: {userDto.PhoneNumber}");

            return userDtoStr.ToString();
        }
    }
}