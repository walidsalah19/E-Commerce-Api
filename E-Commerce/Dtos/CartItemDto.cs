namespace E_Commerce.Dtos
{
    public class CartItemDto : CardItemDtoManage
    {
        public int CartItemId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
