using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ecommerce.Controllers.ControllersApi
{
    public class PanierApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<LigneProduit> Get(String username)
        {
            ApplicationUser user = new ApplicationUser();
            user = db.Users.Where(x => x.UserName.Equals(username)).FirstOrDefault();
            if (user != null)
            {
                Panier panier = user.Paniers.Last();
                var lignePaniers = db.LignePaniers.Where(x => x.id_panier == panier.id);
                List<LigneProduit> ligneProduits = new List<LigneProduit>();
                foreach(LignePanier lp in lignePaniers)
                {
                    ligneProduits.Add(new LigneProduit()
                    {
                        id = lp.Produit.id,
                        name = lp.Produit.name,
                        image = lp.Produit.image,
                        qantite = lp.quantite,
                        prix = lp.Produit.details().deal_price
                    });
                }
                return ligneProduits;
            }
            return null;
        }
    }

}
