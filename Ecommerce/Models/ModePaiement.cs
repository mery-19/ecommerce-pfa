using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class ModePaiement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Libele requis", AllowEmptyStrings = false)]
        [Display(Name = "Libele")]
        public string libele { get; set; }

        [Display(Name = "Description")]
        [Column(TypeName = "Text")]
        public string description { get; set; }

        public virtual ICollection<Commande> Commandes { get; set; }

    }
}