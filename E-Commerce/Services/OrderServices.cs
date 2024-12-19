using E_Commerce.Dtos;
using E_Commerce.Interfaces.RepoInterfaces;
using E_Commerce.Interfaces.ServicesInterfaces;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly ICardItemRepo cardItemRepo;
        private readonly IOrderRepo orderRepo;

        public OrderServices(ICardItemRepo cardItemRepo, IOrderRepo orderRepo)
        {
            this.cardItemRepo = cardItemRepo;
            this.orderRepo = orderRepo;
        }

        public string cancelOrder(int orderId)
        {
            var result= orderRepo.cancelOrder(orderId);
            if(result.Equals("Success"))
            {
                orderRepo.SaveChanges();
                return result;
            }
            return result;
        }

        public async Task<string> createOrder(string userId)
        {
            try
            {
                var cartItems = await cardItemRepo.GetCardItems(userId);
                if (cartItems == null || !cartItems.Any())
                    throw new Exception("Cart is empty.");
                // Create the order
                var order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.UtcNow,
                    TotalAmount = cartItems.Sum(ci => ci.Quantity * ci.Product.Price),
                    Status="New",
                    VendorId=cartItems.First().Product.VederId,
                    OrderItems = cartItems.Select(ci => new OrderItem
                    {
                        ProductId = ci.ProductId,
                        Quantity = ci.Quantity,
                        Price = ci.Product.Price,
                        
                    }).ToList()
                };
                var createResult =await orderRepo.createOrder(order);
                if(createResult.Equals("Success"))
                {
                    var deleteResult = cardItemRepo.RemoveRangeItem(cartItems);
                    if(deleteResult.Result.Equals("Success"))
                    {
                        cardItemRepo.SaveChanges();
                        orderRepo.SaveChanges();
                        return "Success";
                    }
                    createResult = deleteResult.Result;
                }
                return createResult;
            }
            catch(Exception e)
            {
                return e.Message;
            }

        }

        public List<OrderDto> GetOrdersForUser(string userId)
        {
            var orders = orderRepo.GetOrdersForUser(userId).Select(x => new OrderDto
            {
                OrderDate=x.OrderDate,
                OrderId=x.OrderId,
                Status=x.Status,
                TotalAmount=x.TotalAmount,
                UserName=x.User.UserName,
                VindorName=x.Vendor.UserName,
                OrderItems=x.OrderItems.Select(o=> new OrderitemDto
                {
                    OrderItemId=o.OrderItemId,
                    Price=o.Price,
                    Description=o.Product.Description,
                    ImageUrl = $"images/{o.Product.ImageUrl}",
                    Name=o.Product.Name,
                    ProductId=o.Product.ProductId,
                   Quantity=o.Quantity,
                   Stock=o.Product.Stock

                }).ToList(),
            }).ToList();
            return orders;
        }

        public List<OrderDto> GetOrdersForVendor(string VendorId)
        {
            var orders = orderRepo.GetOrdersForVendor(VendorId).Select(x => new OrderDto
            {
                OrderDate = x.OrderDate,
                OrderId = x.OrderId,
                Status = x.Status,
                TotalAmount = x.TotalAmount,
                UserName = x.User.UserName,
                VindorName = x.Vendor.UserName,
                OrderItems = x.OrderItems.Select(o => new OrderitemDto
                {
                    OrderItemId = o.OrderItemId,
                    Price = o.Price,
                    Description = o.Product.Description,
                    ImageUrl = $"images/{o.Product.ImageUrl}",
                    Name = o.Product.Name,
                    ProductId = o.Product.ProductId,
                    Quantity = o.Quantity,
                    Stock = o.Product.Stock

                }).ToList(),
            }).ToList();
            return orders;
        }

        public string UpdateOrderStatus(string status, int orderid)
        {
            var result = orderRepo.UpdateOrderStatus(status,orderid);
            if (result.Equals("Success"))
            {
                orderRepo.SaveChanges();
                return result;
            }
            return result;
        }
    }
}
