using E_Commerce.Helpers;
using E_Commerce.Validations;

namespace E_Commerce.Dtos
{
    public class CouponDto
    {
        public string Code { get; set; }
        public decimal DiscountPercentage { get; set; }
        [FromDate]
        public DateTime ValidFrom { get; set; }
        [ToDate("ValidFrom",ErrorMessage ="please select date after the start data")]
        public DateTime ValidTo { get; set; }
    }
}
