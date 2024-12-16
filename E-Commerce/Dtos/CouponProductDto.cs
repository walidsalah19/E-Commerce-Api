namespace E_Commerce.Dtos
{
    public class CouponProductDto : CouponDto
    {
      public List<ProductDto> products { get; set; }
    }
}
