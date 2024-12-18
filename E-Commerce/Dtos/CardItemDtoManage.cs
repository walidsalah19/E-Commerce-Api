using E_Commerce.Models;

namespace E_Commerce.Dtos
{
    public class CardItemDtoManage
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
