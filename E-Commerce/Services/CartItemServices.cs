using E_Commerce.Dtos;
using E_Commerce.Interfaces.RepoInterfaces;
using E_Commerce.Interfaces.ServicesInterfaces;
using E_Commerce.Models;

namespace E_Commerce.Services
{
    public class CartItemServices : ICardItemServises
    {
        private readonly ICardItemRepo cardItemRepo;
        private readonly ICardRepo cardRepo;

        public CartItemServices(ICardItemRepo cardItemRepo, ICardRepo cardRepo)
        {
            this.cardItemRepo = cardItemRepo;
            this.cardRepo = cardRepo;
        }

        public string AddItem(CardItemDtoManage item,string userId)
        {
            var cartId = cardRepo.GetCartId(userId);
            CardItem cardItem = new CardItem
            {
                CartId=cartId,
                ProductId=item.ProductId,
                Quantity=item.Quantity,
                TotalPrice=item.TotalPrice,
            };
            return cardItemRepo.AddItem(cardItem);
        }

        public CartItemDto GetCardItemById(int id, string userId)
        {
            var item = cardItemRepo.GetCardItemById(id, userId);
            if (item != null)
            {
                var dto = new CartItemDto
                {
                    ProductId = item.Product.ProductId,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice,
                    Description = item.Product.Description,
                    ImageUrl = $"images/{item.Product.ImageUrl}",
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    CartItemId=item.CartItemId
                };
                return dto;
            }
            return null;
        }

        public CartItemDto GetCardItemByProductName(string name, string userId)
        {
            var item = cardItemRepo.GetCardItemByProductName(name, userId);
            if (item != null)
            {
                var dto = new CartItemDto
                {
                    ProductId = item.Product.ProductId,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice,
                    Description = item.Product.Description,
                    ImageUrl = $"images/{item.Product.ImageUrl}",
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    CartItemId = item.CartItemId

                };
                return dto;
            }
            return null;
        }

        public async Task<List<CartItemDto>> GetCardItems(string userId)
        {
            var items = await cardItemRepo.GetCardItems(userId);
            var ListItems=items.Select(item => new CartItemDto
            {
                ProductId = item.Product.ProductId,
                Quantity = item.Quantity,
                TotalPrice = item.TotalPrice,
                Description = item.Product.Description,
                ImageUrl = $"images/{item.Product.ImageUrl}",
                Name = item.Product.Name,
                Price = item.Product.Price,
                CartItemId = item.CartItemId

            }).ToList();

            return ListItems;
        }

        public string RemoveItem(int id, string userId)
        {
            return cardItemRepo.RemoveItem(id, userId);
        }

        public void SaveChanges()
        {
            cardItemRepo.SaveChanges();
        }
    }
}
