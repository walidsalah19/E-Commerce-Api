using E_Commerce.Models;

namespace E_Commerce.Interfaces.RepoInterfaces
{
    public interface ICardRepo
    {
        public void AddCart(Card cart);

        public int GetCartId(string userId);

        public void saveChanges();


    }
}
