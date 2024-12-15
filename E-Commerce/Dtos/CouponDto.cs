using E_Commerce.Helpers;

namespace E_Commerce.Dtos
{
    public class CouponDto
    {
        public int CouponId { get; set; }
        public string Code { get; set; }
        public decimal DiscountPercentage { get; set; }
        [FromDate]
        public DateTime ValidFrom { get; set; }
        [ToDate("ValidFrom",ErrorMessage ="please select date after the start data")]
        public DateTime ValidTo { get; set; }
    }
}
