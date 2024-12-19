using AutoMapper;
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
        private readonly IMapper mapper;

        public CartItemServices(ICardItemRepo cardItemRepo, ICardRepo cardRepo, IMapper mapper)
        {
            this.cardItemRepo = cardItemRepo;
            this.cardRepo = cardRepo;
            this.mapper = mapper;
        }

        public string AddItem(CardItemDtoManage item,string userId)
        {
            var cartId = cardRepo.GetCartId(userId);
            CardItem cardItem = new CardItem
            {
                CartId=cartId,
                ProductId=item.ProductId,
                Quantity=item.Quantity,
                TotalPrice= item.Price * item.Quantity,
            };
            return cardItemRepo.AddItem(cardItem);
        }

        public CartItemDto GetCardItemById(int id, string userId)
        {
            var item = cardItemRepo.GetCardItemById(id, userId);
            if (item != null)
            {
                var dto = mapper.Map<CartItemDto>(item);
                return dto;
            }
            return null;
        }

        public CartItemDto GetCardItemByProductName(string name, string userId)
        {
            var item = cardItemRepo.GetCardItemByProductName(name, userId);
            if (item != null)
            {
                var dto = mapper.Map<CartItemDto>(item);
                return dto;
            }
            return null;
        }

        public async Task<List<CartItemDto>> GetCardItems(string userId)
        {
            var items = await cardItemRepo.GetCardItems(userId);
            var ListItems= mapper.Map<List<CartItemDto>>(items);

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
