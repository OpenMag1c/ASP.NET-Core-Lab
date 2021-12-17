using System.Collections.Generic;
using DAL.Models;

namespace DAL.Seeds
{
    public static class SeedDbUsers
    {
        public static readonly IEnumerable<User> Users = new List<User>
        {
            new User()
            {
                Id = 1,
                Email = "admin@gmail.com",
                PasswordHash = "_Aa123456",
                UserName = "admin",
                PhoneNumber = "375447400686"
            }
        };
    }
}
