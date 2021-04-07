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
    public class CommandeApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public IEnumerable<Commande> Get(int id, String username)
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            ApplicationUser user = new ApplicationUser();

            user = db.Users.Where(x => x.UserName.Equals(username)).FirstOrDefault();
            if (user != null)
            {
                var commandes = db.Commandes.Where(x => x.Panier.User.UserName.Equals(user.UserName));
                    commandes = (id == 2) ? commandes.Where(x => x.id_status == id) : ((id==1)?commandes.Where(x => x.id_status == 1):commandes);

                return commandes.OrderByDescending(x => x.date_ajout);
            }
            return null;
        }
        [HttpGet]
        public IEnumerable<LigneProduit> showProductInCommande(int id_commande)
        {
            Commande commande = db.Commandes.Find(id_commande);
            List<LignePanier> lignes = db.LignePaniers.Where(x => x.id_panier == commande.id_panier).ToList();
            List<LigneProduit> lesProduits = new List<LigneProduit>();
            foreach (LignePanier l in lignes)
            {
                LigneProduit my = new LigneProduit();
                my.name = l.Produit.name;
                my.image = l.Produit.image;
                my.quantite = l.quantite;
                my.prix = l.prix_total;
                lesProduits.Add(my);
            }
            return lesProduits;
        }
    }
}
