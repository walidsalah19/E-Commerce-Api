using E_Commerce.Dtos;
using E_Commerce.Models;

namespace E_Commerce.Interfaces.ServicesInterfaces
{
    public interface IWishItemsServices
    {
        public string AddItem(int productId,string userId);
        public string RemoveItem(int id, string userId);
        public WishItemsDto GetItemById(int id, string userId);
        public WishItemsDto GetItemByProductName(string name, string userId);

        public List<WishItemsDto> GetItems(string userId);

        public void SaveChanges();
    }
}
