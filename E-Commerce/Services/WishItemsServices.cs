using E_Commerce.Dtos;
using E_Commerce.Interfaces.RepoInterfaces;
using E_Commerce.Interfaces.ServicesInterfaces;
using E_Commerce.Models;
using E_Commerce.Repostories;

namespace E_Commerce.Services
{
    public class WishItemsServices : IWishItemsServices
    {
        private readonly IWishListItemRepo itemsRepo;
        private readonly IWishListRepo wishListRepo;

        public WishItemsServices(IWishListItemRepo itemsRepo, IWishListRepo wishListRepo)
        {
           this.itemsRepo = itemsRepo;
            this.wishListRepo = wishListRepo;
        }

        public string AddItem(int Id, string userId)
        {
            var wishId = wishListRepo.GetWishListId(userId);
            WishlistItem item = new WishlistItem
            {
                ProductId = Id,
                WishlistId= wishId

            };
            return itemsRepo.AddItem(item);
        }

        public WishItemsDto GetItemById(int id, string userId)
        {
            var item = itemsRepo.GetItemById(id, userId);
            if (item != null)
            {
                var dto = new WishItemsDto
                {
                    ProductId = item.Product.ProductId,
                    Description = item.Product.Description,
                    ImageUrl = $"images/{item.Product.ImageUrl}",
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    Stock=item.Product.Stock,
                    WishlistItemId = item.WishlistItemId
                };
                return dto;
            }
            return null;
        }

        public WishItemsDto GetItemByProductName(string name, string userId)
        {
            var item = itemsRepo.GetItemByProductName(name, userId);
            if (item != null)
            {
                var dto = new WishItemsDto
                {
                    ProductId = item.Product.ProductId,
                    Description = item.Product.Description,
                    ImageUrl = $"images/{item.Product.ImageUrl}",
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    Stock = item.Product.Stock,
                    WishlistItemId = item.WishlistItemId
                };
                return dto;
            }
            return null;
        }

        public List<WishItemsDto> GetItems(string userId)
        {
            var items = itemsRepo.GetItems(userId).Select(item => new WishItemsDto
            {
                ProductId = item.Product.ProductId,
                Description = item.Product.Description,
                ImageUrl = $"images/{item.Product.ImageUrl}",
                Name = item.Product.Name,
                Price = item.Product.Price,
                Stock=item.Product.Stock,
                WishlistItemId = item.WishlistItemId

            }).ToList();

            return items;
        }

        public string RemoveItem(int id, string userId)
        {
            return itemsRepo.RemoveItem(id, userId);
        }

        public void SaveChanges()
        {
            itemsRepo.SaveChanges();
        }
    }
}
