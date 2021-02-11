using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Commentaire
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string id_user { get; set; }
        [ForeignKey("id_user")]
        public virtual ApplicationUser User { get; set; }

        public int id_produit { get; set; }
        [ForeignKey("id_produit")]
        public virtual Produit Produit { get; set; }

        [Display(Name = "Commentaire")]
        [Column(TypeName = "text")]
        public string commmentaire { get; set; }

        [Display(Name = "Date d'ajout")]
        public DateTime date_ajout { get; set; } = DateTime.Now;
    }
}