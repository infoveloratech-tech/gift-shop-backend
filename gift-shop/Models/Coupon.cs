using System.ComponentModel.DataAnnotations;

namespace gift_shop.Models
{
    public class Coupon
    {
        [Key]
        public int coupon_id { get; set; }

        public string coupon_code { get; set; }

        public string discount_type { get; set; }   // e.g. "percentage" or "fixed"

        public decimal discount_value { get; set; }

        public decimal min_order_amount { get; set; }

        public DateTime start_date { get; set; }

        public DateTime end_date { get; set; }

        public int usage_limit { get; set; }

        public int used_count { get; set; }

        public string status { get; set; }   // active / inactive

        public DateTime created_at { get; set; }
    }
}
