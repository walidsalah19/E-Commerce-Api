namespace E_Commerce.Models
{
    public class Vendor : UserApplication
    {
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Coupon> Coupons { get; set; }

    }
}
