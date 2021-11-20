﻿using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Database
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductRating>()
                .HasKey(t => new { t.ProductId, t.UserId });

            modelBuilder.Entity<ProductRating>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.Ratings)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<ProductRating>()
                .HasOne(sc => sc.Product)
                .WithMany(c => c.Ratings)
                .HasForeignKey(sc => sc.ProductId);

            modelBuilder.Entity<Product>().HasIndex(u => new {u.DateCreated, u.Name, u.Platform, u.TotalRating});
            modelBuilder.Entity<Product>().HasData(SeedDbProducts.GetSeedProducts());
        }
    }
}