using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class UserNotification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public float num { get; set; } = 0;

        public Boolean status { get; set; } = false;

        public string id_user { get; set; }
        [ForeignKey("id_user")]
        public virtual ApplicationUser User { get; set; }
    }
}