using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class Shipping
    {
        public int ShippingId { get; set; }
      
        public string TrackingNumber { get; set; }
        public string Carrier { get; set; }
        public string Status { get; set; } // e.g., In Transit, Delivered
        public DateTime EstimatedDeliveryDate { get; set; }
        public DateTime ShippedDate { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
