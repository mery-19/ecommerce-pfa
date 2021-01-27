using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Commande
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Display(Name = "Description")]
        [Column(TypeName = "text")]
        public string description { get; set; }

        [Display(Name = "Date d'ajout")]
        public DateTime date_ajout { get; set; } = DateTime.Now;

        /******** Add foreign keys *************/
        public int id_panier { get; set; }
        [ForeignKey("id_panier")]
        public virtual Panier Panier { get; set; }

        public int id_status { get; set; }
        [ForeignKey("id_status")]
        public virtual StatusCommande StatusCommande { get; set; }

        public int id_mode_livraison { get; set; }
        [ForeignKey("id_mode_livraison")]
        public virtual ModeLivraison ModeLivraison { get; set; }

        public int id_paiement { get; set; }
        [ForeignKey("id_paiement")]
        public virtual ModePaiement ModePaiement { get; set; }



    }
}