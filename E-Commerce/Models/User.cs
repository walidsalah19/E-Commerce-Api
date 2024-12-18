using System.Net;

namespace E_Commerce.Models
{
    public class User : UserApplication
    {
        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public Wishlist Wishlist { get; set; }
        public Card Cart { get; set; }
    }
}
