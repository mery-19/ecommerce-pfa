using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Ecommerce.Models
{
    public class LignePanier
    {
        [Key, Column(Order = 1)]
        public int id_panier { get; set; }
        [ForeignKey("id_panier")]
        public virtual Panier Panier { get; set; }

        [Key, Column(Order = 2)]
        public int id_produit { get; set; }
        [ForeignKey("id_produit")]
        public virtual Produit Produit { get; set; }

        [Display(Name = "Quantité")]
        public int quantite { get; set; }

        [Display(Name = "Prix HT")]
        public float? prix_ht { get; set; }

        [Display(Name = "TVA")]
        public float? prix_tva { get; set; }

        [Display(Name = "Prix total")]
        public float? prix_total { get; set; }
    }
}