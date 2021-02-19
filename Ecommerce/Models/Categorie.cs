using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Categorie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Libele requis", AllowEmptyStrings = false)]
        [Display(Name = "Categorie")]
        public string libele { get; set; }

        [Display(Name = "Upload image")]
        public string image { get; set; }
    }
}