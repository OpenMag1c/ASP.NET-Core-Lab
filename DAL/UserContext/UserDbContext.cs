using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.UserContext
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserDbContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User{Age = 18, Id = 1, Name = "Maxim"});
        }
    }
}