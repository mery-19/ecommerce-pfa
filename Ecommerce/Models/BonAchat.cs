using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class BonAchat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Libele requis", AllowEmptyStrings = false)]
        [Display(Name = "Libele")]
        public string libele { get; set; }

        [Required(ErrorMessage = "Taux de remise requis", AllowEmptyStrings = false)]
        [Display(Name = "Taux de remise (%)")]
        public float taux_remise { get; set; }
    }
}