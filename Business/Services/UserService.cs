using System.Collections.Generic;
using AutoMapper;
using Business.Infrastructure;
using Business.Interfaces;
using DAL.DTO;
using DAL.Models;
using DAL.Repository;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private UserRepository _repo;

        public UserService(UserRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(_repo.GetAll());
        }

        public UserDTO GetUserById(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id пользователя", "");
            var user = _repo.Get(id.Value);
            if (user == null)
                throw new ValidationException("Пользователь не найден", "");
            return new UserDTO() {Age = user.Age, Name = user.Name, Id = user.Id};
        }

        public void AddUser(UserDTO userDto)
        {
            var user = new User
            {
                Age = userDto.Age,
                Id = userDto.Id,
                Name = userDto.Name
            };
            
            _repo.Create(user);
        }

        public void Dispose()
        {
            _repo.Dispose();
        }
    }
}