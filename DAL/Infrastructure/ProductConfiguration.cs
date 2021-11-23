using DAL.Database;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Infrastructure
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasIndex(u => new { u.DateCreated, u.Name, u.Platform, u.TotalRating });
            builder.HasData(SeedDbProducts.GetSeedProducts());
        }
    }
}