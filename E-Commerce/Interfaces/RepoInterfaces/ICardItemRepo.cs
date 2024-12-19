using E_Commerce.Models;

namespace E_Commerce.Interfaces.RepoInterfaces
{
    public interface ICardItemRepo
    {
        public string AddItem(CardItem item);
        public string RemoveItem(int id, string userId);
        public CardItem GetCardItemById(int id, string userId);
        public CardItem GetCardItemByProductName(string name, string userId);

        public Task<List<CardItem>> GetCardItems(string userId);

        public Task<string> RemoveRangeItem(List<CardItem> items);

        public void SaveChanges();

    }
}
