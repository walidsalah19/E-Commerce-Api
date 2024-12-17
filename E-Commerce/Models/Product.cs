namespace E_Commerce.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int? CouponId { get; set; }
        public Coupon Coupon { get; set; }
        public string VederId { get; set; }
        public Vendor vendor { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
