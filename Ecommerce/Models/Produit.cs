using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    
    public class Produit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Le nom de produit requis.", AllowEmptyStrings = false)]
        [Display(Name = "Le nom de produit")]
        public string name { get; set; }

        [Required(ErrorMessage = "La description de produit requis.", AllowEmptyStrings = false)]
        [Display(Name = "Description")]
        [Column(TypeName = "text")]
        public string description { get; set; }

        [Required(ErrorMessage = "Prix d'achat requis", AllowEmptyStrings = false)]
        [Display(Name = "Prix d'achat")]
        public float prix_achat { get; set; }

        [Required(ErrorMessage = "Prix de vente", AllowEmptyStrings = false)]
        [Display(Name = "Prix de vente")]
        public float prix_vente { get; set; }

        [Display(Name = "La quantite")]
        public int quantite_stock { get; set; }

        [Display(Name = "TVA")]
        public int tva { get; set; }

        [Required(ErrorMessage = "Image de produit requis.", AllowEmptyStrings = false)]
        [Display(Name = "Upload image")]
        public string image { get; set; }

        [Display(Name = "Date d'ajout")]
        public DateTime? date_ajout { get ; set; }

        [Display(Name = "Date de modification")]
        public DateTime? date_modification { get; set; }

        /******** Add foreign keys *************/

        public string id_user { get; set; }
        [ForeignKey("id_user")]
        public virtual ApplicationUser User { get; set; }

        public int id_categorie { get; set; }
        [ForeignKey("id_categorie")]
        public virtual Categorie Categorie { get; set; }

        public int? id_promotion { get; set; }
        [ForeignKey("id_promotion")]
        public virtual Promotion Promotion { get; set; }

        //to know how many times the product commande
        public virtual ICollection<Commande> Commandes { get; set; }

    }
}