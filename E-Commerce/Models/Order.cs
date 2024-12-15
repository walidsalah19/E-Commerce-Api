namespace E_Commerce.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string VendorId { get; set; }
        public Vendor Vendor { get; set; }
       
        public Shipping Shipping { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public string Status { get; set; }
    }
}
