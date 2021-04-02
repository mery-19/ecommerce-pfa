using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Ecommerce.Controllers.ControllersApi
{
    public class PanierApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
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
                        quantite = lp.quantite,
                        prix = lp.Produit.details().deal_price,
                        quantite_disponible = lp.Produit.quantite_stock
                    }); ;
                }
                return ligneProduits;
            }
            return null;
        }

        [HttpGet]
        [ResponseType(typeof(LignePanier))]
        public IHttpActionResult Get(String id_user,int id_produit, int quantite)
        {
            LignePanier ligne = db.LignePaniers.Where(x => x.Panier.id_user == id_user && x.id_produit == id_produit).ToList().LastOrDefault();
            ligne.quantite = quantite;
            db.Entry(ligne).State = EntityState.Modified;
            int res = db.SaveChanges();
            if (res == 1) return Ok(ligne);
            else return NotFound();
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        public IHttpActionResult AddToPanier(String name, int id_produit, int quantite)
        {
            
            ApplicationUser user = db.Users.Where(x => x.UserName.Equals(name)).FirstOrDefault();
            if (user != null)
            {
                Panier panier = user.Paniers.Last();

                Produit produit = db.Produits.Find(Convert.ToInt32(id_produit));
                LignePanier ligne = new LignePanier();
                ligne.id_panier = panier.id;
                ligne.id_produit = Convert.ToInt32(id_produit);
                ligne.quantite = Convert.ToInt32(quantite);
                LignePanier exist = db.LignePaniers.Find(ligne.id_panier, ligne.id_produit);
                if (exist == null)
                {
                    db.LignePaniers.Add(ligne);
                }
                else
                {
                    exist.quantite = ligne.quantite;
                    db.Entry(exist).State = EntityState.Modified;
                }
                int res = db.SaveChanges();

                if (res != 0)
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }

            }
            else
            {
                return Ok(false);

            }
        }


        [HttpGet]
        [ResponseType(typeof(bool))]

        public IHttpActionResult DeleleLignePanier(String id_user, int id_produit)
        {

            LignePanier ligne = db.LignePaniers.Where(x => x.Panier.id_user == id_user && x.id_produit == id_produit).ToList().LastOrDefault();
            db.LignePaniers.Remove(ligne);
            int res = db.SaveChanges();

            if (res != 0)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
    }

}
