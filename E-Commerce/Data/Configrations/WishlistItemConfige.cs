using E_Commerce.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data.Configrations
{
    public class WishlistItemConfige : IEntityTypeConfiguration<WishlistItem>
    {
        public void Configure(EntityTypeBuilder<WishlistItem> builder)
        {
            builder.ToTable("WishlistItem");
            builder.HasKey(x => x.WishlistItemId);
            builder.Property(x => x.WishlistItemId).ValueGeneratedOnAdd();
         
        }
    }
}
