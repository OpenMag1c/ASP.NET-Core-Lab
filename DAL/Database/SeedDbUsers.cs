using System.Threading.Tasks;
using DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace DAL.Database
{
    public class SeedDbUsers
    {
        public static async Task InitSeedDbAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            const string AdminEmail = "admin@gmail.com";
            const string Password = "_Aa123456";
            if (await roleManager.FindByNameAsync(Roles.Admin) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>(Roles.Admin));
            }
            if (await roleManager.FindByNameAsync(Roles.User) == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>(Roles.User));
            }
            if (await userManager.FindByNameAsync(AdminEmail) == null)
            {
                User admin = new User { Email = AdminEmail, UserName = "Admin", EmailConfirmed = true};
                IdentityResult result = await userManager.CreateAsync(admin, Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Roles.Admin);
                }
            }
        }
    }
}
