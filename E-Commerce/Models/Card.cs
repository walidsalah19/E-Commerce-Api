using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class Card
    {
        public int CartId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<CardItem> CartItems { get; set; }
    }
}
