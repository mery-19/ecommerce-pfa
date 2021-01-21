using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Promotion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Display(Name = "Libele")]
        public string libele { get; set; }

        [Display(Name = "Description")]
        [Column(TypeName = "text")]
        public String description { get; set; }

        [Required(ErrorMessage = "Taux de promotion requis", AllowEmptyStrings = false)]
        [Display(Name = "Taux de promotion")]
        public float taux_promotion { get; set; }

        [Display(Name = "Date d'expiration")]
        public DateTime? date_expiration { get; set; }

        [Display(Name = "Date d'ajout")]
        public DateTime date_ajout { get; set; } = DateTime.Now;

        [Display(Name = "Date de modification")]
        public DateTime? date_modification { get; set; }

        public virtual ICollection<Produit> Produits { get; set; }

    }
}