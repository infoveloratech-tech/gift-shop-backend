namespace gift_shop.Models
{
    public class Supplier
    {
        public int supplier_id { get; set; }

        public string? supplier_name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }

        public string? address { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? postal_code { get; set; }
        public string? country { get; set; }

        public string? status { get; set; }

        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}