using E_Commerce.Data;
using E_Commerce.Interfaces.RepoInterfaces;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repostories
{
    public class CardItemRepo : ICardItemRepo
    {
        private readonly AppDbContext context;

        public CardItemRepo(AppDbContext context)
        {
            this.context = context;
        }

        public string AddItem(CardItem item)
        {
            try
            {
                context.CartItems.Add(item);
                return "Success";
            }catch(Exception e)
            {
                return e.Message;
            }
        }

        public CardItem GetCardItemById(int id, string userId)
        {
            var item = context.CartItems.Include(x=>x.Cart).Include(x => x.Product).FirstOrDefault(x=>x.CartItemId==id&& x.Cart.UserId.Equals(userId));
            return item;
        }

        public CardItem GetCardItemByProductName(string name, string userId)
        {
            var item = context.CartItems.Include(x => x.Cart).Include(x=>x.Product).FirstOrDefault(x => x.Product.Name.Equals(name) && x.Cart.UserId.Equals(userId));
            return item;
        }

        public List<CardItem> GetCardItems(string userId)
        {
            var item = context.CartItems.Include(x => x.Cart).Include(x=>x.Product).Where(x =>x.Cart.UserId.Equals(userId)).ToList();
            return item;
        }

        public string RemoveItem(int id, string userId)
        {
            try
            {
                var item = GetCardItemById(id, userId);
                if (item !=null)
                {
                    context.CartItems.Remove(item);
                    return "Success";
                }
                return "Not Found";
            }
            catch(Exception e)
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
