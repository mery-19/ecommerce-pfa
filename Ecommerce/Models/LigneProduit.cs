using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class LigneProduit
    {
        public int id;
        public string name { get; set; }
        public string image { get; set; }
        public int qantite { get; set; }
        public float? prix { get; set; }
    }
}