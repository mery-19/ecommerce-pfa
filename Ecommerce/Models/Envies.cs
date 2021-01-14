using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Envies
    {
        [Key, Column(Order = 1)]
        public string id_user { get; set; }
        [ForeignKey("id_user")]
        public virtual ApplicationUser User { get; set; }

        [Key, Column(Order = 2)]
        public int id_produit { get; set; }
        [ForeignKey("id_produit")]
        public virtual Produit Produit { get; set; }

        public virtual ICollection<Produit> Produits { get; set; }

    }
}