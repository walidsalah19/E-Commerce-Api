using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Dtos
{
    public class ManageProductDto:ProductDto
    {
        public int? CouponId { get; set; }
        public int CategoryId { get; set; }
    }
}
