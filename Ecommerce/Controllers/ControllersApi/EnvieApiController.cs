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
    public class EnvieApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        public IEnumerable<LigneProduit> Get(String username)
        {
            try
            {
                var envies = db.Envies.Where(x => x.User.UserName == username).Include(e => e.Produit).Include(e => e.User);
                List<LigneProduit> lignes = new List<LigneProduit>();
                foreach (Envies e in envies)
                {
                    lignes.Add(new LigneProduit()
                    {
                        id = e.Produit.id,
                        name = e.Produit.name,
                        image = e.Produit.image,
                        prix = e.Produit.details().deal_price,
                        quantite_disponible = e.Produit.quantite_stock
                    });
                }
                return lignes;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

    }
}
