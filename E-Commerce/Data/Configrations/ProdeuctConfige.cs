using E_Commerce.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Configrations
{
    public class ProdeuctConfige : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Prodeuct");
            builder.HasKey(x => x.ProductId);
            builder.Property(x => x.ProductId).ValueGeneratedOnAdd();

            builder.HasIndex(x => x.Name).IsUnique(false);


            builder.HasOne(x => x.Category)
                .WithMany(y => y.Products)
                .HasForeignKey(y => y.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(x => x.vendor)
                .WithMany(y => y.Products)
                .HasForeignKey(y => y.VederId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasMany(x => x.OrderItems)
                .WithOne(y => y.Product)
                .HasForeignKey(y => y.ProductId)
                 .OnDelete(DeleteBehavior.NoAction); 

            builder.HasMany(x => x.Reviews)
               .WithOne(y => y.Product)
               .HasForeignKey(y => y.ProductId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.CartItems)
               .WithOne(y => y.Product)
               .HasForeignKey(y => y.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.WishlistItems)
              .WithOne(y => y.Product)
              .HasForeignKey(y => y.ProductId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Coupon)
               .WithMany(x => x.products)
               .HasForeignKey(x => x.CouponId)
               .IsRequired(false);

        }
    }
}
