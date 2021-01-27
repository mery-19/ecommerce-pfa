using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Panier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string id_user { get; set; }
        [ForeignKey("id_user")]
        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Prix total")]
        public float prix_total { get; set; } = 0;

        [Display(Name = "TVA")]
        public float tva { get; set; } = 0;

        public virtual ICollection<LignePanier> LignePaniers { get; set; }

    }
}