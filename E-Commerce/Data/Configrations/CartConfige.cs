using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configrations
{
    public class CartConfige : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Cart");
            builder.HasKey(x => x.CartId);
            builder.Property(x => x.CartId).ValueGeneratedOnAdd();

            

            builder.HasMany(x => x.CartItems)
              .WithOne(y => y.Cart)
              .HasForeignKey(y=>y.CartId)
              .IsRequired();

            builder.HasOne(c => c.User)
            .WithOne(u => u.Cart)
            .HasForeignKey<Cart>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
