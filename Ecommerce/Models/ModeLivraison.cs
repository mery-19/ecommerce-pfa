using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class ModeLivraison
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Display(Name = "Mode de livraison")]
        public string libele { get; set; }

        /*   [Display(Name = "Frais de livraison")]
           public float frais_livraison { get; set; }*/

        public virtual ICollection<Commande> Commandes { get; set; }

    }
}