using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class ProduitDetails
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public float real_price { get; set; }
        public float? deal_price { get; set; }
        public float? save_price { get; set; }
        public float image { get; set; }
        public float quantite { get; set; }
        public DateTime? date_expiration_promo { get; set; }
    }
}