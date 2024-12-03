
using E_Commerce.Filters;

namespace E_Commerce.Models
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string Code { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public string VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
