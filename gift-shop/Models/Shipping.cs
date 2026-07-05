using System.ComponentModel.DataAnnotations;

namespace gift_shop.Models
{
    public class Shipping
    {
        [Key]
        public int shipping_id { get; set; }

        public int order_id { get; set; }

        public string courier_name { get; set; }

        public string tracking_number { get; set; }

        public DateTime? shipping_date { get; set; }

        public DateTime? delivery_date { get; set; }

        public string shipping_status { get; set; }  // e.g. pending, shipped, delivered

        public DateTime created_at { get; set; }
    }
}
