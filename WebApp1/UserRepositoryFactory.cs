using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Repository;

namespace WebApp1
{
    public interface IUserRepositoryFactory
    {
        UserRepository Get();
    }

    public class UserRepositoryFactory : IUserRepositoryFactory
    {
        public UserRepository Get()
        {
            throw new NotImplementedException();
        }
    }
}
