using DAL.Model;
using System;


namespace DAL.Repository
{
    public class UserRepository:IRepository
    {
        public User Get()
        {
            var user = new User { Age = 18, Name = "Ilya", Id = new Guid() }; // from db
            return user;        
        }
    }
}
