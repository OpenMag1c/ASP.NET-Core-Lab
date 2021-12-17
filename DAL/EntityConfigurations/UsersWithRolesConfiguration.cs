using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EntityConfigurations
{
    public class UsersWithRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
        {
            var identityUserRole = new IdentityUserRole<int>
            {
                RoleId = 1,
                UserId = 1
            };

            builder.HasData(identityUserRole);
        }
    }
}