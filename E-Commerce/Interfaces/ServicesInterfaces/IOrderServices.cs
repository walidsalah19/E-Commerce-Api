using System.Collections.Generic;
using System.Threading.Tasks;
using E_Commerce.Dtos;


namespace E_Commerce.Interfaces.ServicesInterfaces
{
    public interface IOrderServices
    {
        public Task<string> createOrder(string userId);
        public string cancelOrder(int orderId);

        public List<OrderDto> GetOrdersForUser(string userId);
        public List<OrderDto> GetOrdersForVendor(string VendorId);

        public string UpdateOrderStatus(string status, int orderid);
    }
}
