namespace E_Commerce.Models
{
    public class CardItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public Card Cart { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
