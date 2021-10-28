using System;
using System.Collections.Generic;
using AutoMapper;
using Business.DTO;
using Business.Infrastructure;
using Business.Interfaces;
using DAL.Models;
using DAL.Repository;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repo;

        public UserService(IRepository<User> repo)
        {
            _repo = repo;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDto>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDto>>(_repo.GetAll());
        }

        public UserDto GetUserById(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id пользователя", "");
            var user = _repo.Get(id.Value);
            if (user == null)
                throw new ValidationException("Пользователь не найден", "");
            return new UserDto() {Age = user.Age, Name = user.Name, Id = user.Id};
        }

        public void AddUser(UserDto userDto)
        {
            var user = new User
            {
                Age = userDto.Age,
                Id = userDto.Id,
                Name = userDto.Name
            };
            
            _repo.Create(user);
        }
    }
}