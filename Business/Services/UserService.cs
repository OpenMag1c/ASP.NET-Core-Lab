using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using Business.DTO;
using Business.Infrastructure;
using Business.Interfaces;
using DAL.Repository;
using Microsoft.AspNetCore.Identity;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<IdentityUser<int>> _repo;

        public UserService(IRepository<IdentityUser<int>> repo)
        {
            _repo = repo;
        }

        public ClaimsIdentity GetIdentity(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        //public IEnumerable<UserDto> GetAllUsers()
        //{
        //    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<IdentityUser<int>, UserDto>()).CreateMapper();
        //    return mapper.Map<IEnumerable<IdentityUser<int>>, List<UserDto>>(_repo.GetAll());
        //}

        //public UserDto GetUserById(int? id)
        //{
        //    if (id == null)
        //        throw new ValidationException("Не установлено id пользователя", "");
        //    var user = _repo.Get(id.Value);
        //    if (user == null)
        //        throw new ValidationException("Пользователь не найден", "");
        //    return new UserDto() {Name = user.UserName, Id = user.Id};
        //}

        //public void AddUser(UserDto userDto)
        //{
        //    var user = new IdentityUser<int>
        //    {
        //        Id = userDto.Id,
        //        UserName = userDto.Name
        //    };

        //    _repo.Create(user);
        //}
    }
}