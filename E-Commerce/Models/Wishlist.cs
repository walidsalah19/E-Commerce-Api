using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; }
       
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<WishlistItem> WishlistItems { get; set; }
    }
}
