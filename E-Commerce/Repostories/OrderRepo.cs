using E_Commerce.Data;
using E_Commerce.Interfaces.RepoInterfaces;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repostories
{
    public class OrderRepo : IOrderRepo
    {
        private readonly AppDbContext context;

        public OrderRepo(AppDbContext context)
        {
            this.context = context;
        }

        public string cancelOrder(int orderId)
        {
            try
            {
                var order = GetOrder(orderId);
                if (order != null)
                {
                    context.Orders.Remove(order);
                    return "Success";
                }
                return "Not Found";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> createOrder(Order order)
        {
            try
            {
                
              await context.Orders.AddAsync(order);
              return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public List<Order> GetOrdersForUser(string userId)
        {
            return context.Orders.Include(x=>x.User).Include(x=>x.Vendor).Include(x=>x.OrderItems).ThenInclude(y=>y.Product).Where(x => x.UserId.Equals(userId)).ToList();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public string UpdateOrderStatus(string status, int orderId)
        {
            try
            {
                var order = GetOrder(orderId);
                if (order != null)
                {
                    order.Status = status;
                    return "Success";
                }
                return "Not Found";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public Order GetOrder(int orderId)
        {
            return context.Orders.FirstOrDefault(x => x.OrderId == orderId);
        }

        public List<Order> GetOrdersForVendor(string VendorId)
        {
            return context.Orders.Include(x => x.User).Include(x => x.Vendor).Include(x => x.OrderItems).ThenInclude(y => y.Product).Where(x => x.VendorId.Equals(VendorId)).ToList();
        }
    }
}
