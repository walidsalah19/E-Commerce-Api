using E_Commerce.Dtos;
using E_Commerce.Models;

namespace E_Commerce.Interfaces.RepoInterfaces
{
    public interface IOrderRepo
    {
        public Task<string> createOrder(Order order);
        public string cancelOrder(int orderId);
        public List<Order> GetOrdersForUser(string userId);
        public string UpdateOrderStatus(string status, int orderid);
        public List<Order> GetOrdersForVendor(string VendorId);

        public Order GetOrder(int orderId);
        public void SaveChanges();
    }
}
