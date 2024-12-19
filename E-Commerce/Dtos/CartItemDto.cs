namespace E_Commerce.Dtos
{
    public class CartItemDto : CardItemDtoManage
    {
        public int CartItemId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
