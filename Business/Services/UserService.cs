

using Business.Interfaces;
using DAL.Model;
using DAL.Repository;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private IRepository _userRepo;
        public UserService(IRepository userRepository)
        {
            _userRepo = userRepository;
        }


        public string GetUserByName(string name)
        {
            return _userRepo.Get().Name;

        }
    }
}
