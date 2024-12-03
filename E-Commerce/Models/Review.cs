using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string? UserId { get; set; }
        public User User { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public int Rating { get; set; }
       
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
