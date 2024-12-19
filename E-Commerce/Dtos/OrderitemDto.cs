namespace E_Commerce.Dtos
{
    public class OrderitemDto : ProductDto
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        
    }
}
