using E_Commerce.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Configrations
{
    public class WishlistConfige : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.ToTable("Wishlist");
            builder.HasKey(x => x.WishlistId);
            builder.Property(x => x.WishlistId).ValueGeneratedOnAdd();

           

            builder.HasMany(x => x.WishlistItems)
                .WithOne(y => y.Wishlist)
                .HasForeignKey(x => x.WishlistId)
                .OnDelete(DeleteBehavior.Cascade);

           builder.HasOne(c => c.User)
               .WithOne(u => u.Wishlist)
               .HasForeignKey<Wishlist>(c => c.UserId)
               .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
