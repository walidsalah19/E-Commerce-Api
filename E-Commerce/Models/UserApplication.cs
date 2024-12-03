using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Models
{
    public class UserApplication :IdentityUser
    {
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsVendor { get; set; }
    }
}
