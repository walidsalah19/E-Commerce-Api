using E_Commerce.Dtos;
using E_Commerce.Models;

namespace E_Commerce.Interfaces.ServicesInterfaces
{
    public interface ICardItemServises
    {
        public string AddItem(CardItemDtoManage item, string userId);
        public string RemoveItem(int id, string userId);
        public CartItemDto GetCardItemById(int id, string userId);
        public CartItemDto GetCardItemByProductName(string name, string userId);

        public List<CartItemDto> GetCardItems(string userId);
        public void SaveChanges();

    }
}
