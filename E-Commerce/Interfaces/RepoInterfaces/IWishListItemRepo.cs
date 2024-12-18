using E_Commerce.Models;

namespace E_Commerce.Interfaces.RepoInterfaces
{
    public interface IWishListItemRepo
    {
        public string AddItem(WishlistItem item);
        public string RemoveItem(int id, string userId);
        public WishlistItem GetItemById(int id, string userId);
        public WishlistItem GetItemByProductName(string name, string userId);

        public List<WishlistItem> GetItems(string userId);

        public void SaveChanges();
    }
}
