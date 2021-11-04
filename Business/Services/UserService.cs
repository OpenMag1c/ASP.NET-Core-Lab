using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.DTO;
using Business.Exception;
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

        public UserService(IRepository<User> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public UserDTO GetUserById(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id пользователя", "");
            var user = _repo.Get(id.Value);
            if (user == null)
                throw new ValidationException("Пользователь не найден", "");
            return _mapper.Map<UserDTO>(user);
        }

        public async void Register(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            _repo.RegisterUser(user);
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

        //public async Task<User> FindUserByName(string name) => await _repo.FindUserByName(name);

        //public IEnumerable<UserDTO> GetAllUsers()
        //{
        //    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<IdentityUser<int>, UserDTO>()).CreateMapper();
        //    return mapper.Map<IEnumerable<IdentityUser<int>>, List<UserDTO>>(_repo.GetAll());
        //}

        //public void AddUser(UserDTO userDto)
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