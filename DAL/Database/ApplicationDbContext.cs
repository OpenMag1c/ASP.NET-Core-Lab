using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User { Age = 18, Id = 1, Name = "Maxim" });
            modelBuilder.Entity<User>().HasData(new User { Age = 29, Id = 2, Name = "Jerry" });
        }
    }
}