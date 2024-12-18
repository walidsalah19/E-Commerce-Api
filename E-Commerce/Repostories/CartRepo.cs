using E_Commerce.Data;
using E_Commerce.Interfaces.RepoInterfaces;
using E_Commerce.Models;

namespace E_Commerce.Repostories
{
    public class CartRepo : ICardRepo
    {
        private readonly AppDbContext context;

        public CartRepo(AppDbContext context)
        {
            this.context = context;
        }

        public void AddCart(Card cart)
        {
            context.Carts.Add(cart);
        }

        public int GetCartId(string userId)
        {
            var cart = context.Carts.FirstOrDefault(x=>x.UserId.Equals(userId));

            return cart.CartId;
        }

        public void saveChanges()
        {
            context.SaveChanges();
        }
    }
}
