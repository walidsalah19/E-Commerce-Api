using E_Commerce.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Configrations
{
    public class CouponConfige : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.ToTable("Coupon");
            builder.HasKey(x => x.CouponId);
            builder.Property(x => x.CouponId).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Vendor)
              .WithMany(y => y.Coupons)
              .HasForeignKey(y => y.VendorId)
              .OnDelete(DeleteBehavior.Cascade)
              .IsRequired();
            
        }
    }
}
