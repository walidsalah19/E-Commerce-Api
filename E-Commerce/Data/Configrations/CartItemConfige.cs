using E_Commerce.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Configrations
{
    public class CartItemConfige : IEntityTypeConfiguration<CardItem>
    {
        public void Configure(EntityTypeBuilder<CardItem> builder)
        {
            builder.ToTable("CartItem");
            builder.HasKey(x => x.CartItemId);
            builder.Property(x => x.CartItemId).ValueGeneratedOnAdd();
           
        }
    }
}
