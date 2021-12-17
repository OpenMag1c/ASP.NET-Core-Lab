using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DAL.Seeds
{
    public static class SeedDbIdentityRoles
    {
        public static readonly IEnumerable<IdentityRole<int>> Roles = new List<IdentityRole<int>>()
        {
            new IdentityRole<int>()
            {
                Id = 1,
                Name = Enum.Roles.Admin,
                NormalizedName = Enum.Roles.Admin.ToUpper()
            },
            new IdentityRole<int>()
            {
                Id = 2,
                Name = Enum.Roles.User,
                NormalizedName = Enum.Roles.User.ToUpper()
            }
        };
    }
}