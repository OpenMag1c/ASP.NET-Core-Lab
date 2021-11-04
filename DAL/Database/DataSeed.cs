using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Database;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;

namespace EFData
{
    public class DataSeed
    {
        public static async Task SeedDataAsync(ApplicationDbContext context, UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<User>
                                {
                                    new User
                                        {
                                            UserName = "TestUserFirst",
                                            Email = "testuserfirst@test.com"
                                        },

                                    new User
                                        {
                                            UserName = "TestUserSecond",
                                            Email = "testusersecond@test.com"
                                        }
                                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "qwerty");
                }
            }
        }
    }
}