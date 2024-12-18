using E_Commerce.Data;
using E_Commerce.Interfaces.RepoInterfaces;
using E_Commerce.Models;

namespace E_Commerce.Repostories
{
    public class WishListRepo : IWishListRepo
    {
        private readonly AppDbContext context;

        public WishListRepo(AppDbContext context)
        {
            this.context = context;
        }

        public void AddWishList(Wishlist wishlist)
        {
            context.Wishlists.Add(wishlist);
        }

        public int GetWishListId(string userId)
        {
            var wish = context.Wishlists.SingleOrDefault(x => x.UserId.Equals(userId));

            return wish.WishlistId;
        }

        public void saveChanges()
        {
            context.SaveChanges();
        }
    }
}
