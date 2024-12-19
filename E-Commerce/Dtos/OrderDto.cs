using E_Commerce.Models;

namespace E_Commerce.Dtos
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string UserName { get; set; }

        public string VindorName { get; set; }

        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<OrderitemDto> OrderItems { get; set; }

        public string Status { get; set; }
    }
}
