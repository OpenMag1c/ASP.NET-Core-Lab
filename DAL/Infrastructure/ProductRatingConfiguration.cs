using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Infrastructure
{
    public class ProductRatingConfiguration : IEntityTypeConfiguration<ProductRating>
    {
        public void Configure(EntityTypeBuilder<ProductRating> builder)
        {
            builder.HasKey(t => new { t.ProductId, t.UserId });

            builder.HasOne(sc => sc.User)
                .WithMany(s => s.Ratings)
                .HasForeignKey(sc => sc.UserId);

            builder.HasOne(sc => sc.Product)
                .WithMany(c => c.Ratings)
                .HasForeignKey(sc => sc.ProductId);
        }
    }
}