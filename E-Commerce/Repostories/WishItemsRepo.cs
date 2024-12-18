using E_Commerce.Data;
using E_Commerce.Interfaces.RepoInterfaces;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repostories
{
    public class WishItemsRepo : IWishListItemRepo
    {
        public readonly AppDbContext context;

        public WishItemsRepo(AppDbContext context)
        {
            this.context = context;
        }

        public string AddItem(WishlistItem item)
        {
            try
            {
                context.WishlistItems.Add(item);
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public WishlistItem GetItemById(int id, string userId)
        {
            var item = context.WishlistItems.Include(x => x.Wishlist).Include(x => x.Product).FirstOrDefault(x => x.WishlistItemId == id && x.Wishlist.UserId.Equals(userId));
            return item;
        }

        public WishlistItem GetItemByProductName(string name, string userId)
        {
            var item = context.WishlistItems.Include(x => x.Wishlist).Include(x => x.Product).FirstOrDefault(x => x.Product.Name.Equals(name) && x.Wishlist.UserId.Equals(userId));
            return item;
        }

        public List<WishlistItem> GetItems(string userId)
        {
            var item = context.WishlistItems.Include(x => x.Wishlist).Include(x => x.Product).Where(x => x.Wishlist.UserId.Equals(userId)).ToList();
            return item;
        }

        public string RemoveItem(int id, string userId)
        {
            try
            {
                var item = GetItemById(id, userId);
                if (item != null)
                {
                    context.WishlistItems.Remove(item);
                    return "Success";
                }
                return "Not Found";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
