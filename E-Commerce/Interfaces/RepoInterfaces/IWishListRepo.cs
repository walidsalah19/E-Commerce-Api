using E_Commerce.Models;

namespace E_Commerce.Interfaces.RepoInterfaces
{
    public interface IWishListRepo
    {
        public void AddWishList(Wishlist wishlist);

        public int GetWishListId(string userId);

        public void saveChanges();
    }
}
