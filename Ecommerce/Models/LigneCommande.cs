using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Ecommerce.Models
{
    public class LigneCommande
    {
        [Key, Column(Order = 1)]
        public string id_commande { get; set; }
        [ForeignKey("id_commande")]
        public virtual ApplicationUser User { get; set; }

        [Key, Column(Order = 2)]
        public int id_produit { get; set; }
        [ForeignKey("id_produit")]
        public virtual Produit Produit { get; set; }

        [Display(Name = "Quantité")]
        public float quantite { get; set; }

        [Display(Name = "Prix total")]
        public float prix_total { get; set; }

        [Display(Name = "TVA")]
        public float tva { get; set; }
    }
}