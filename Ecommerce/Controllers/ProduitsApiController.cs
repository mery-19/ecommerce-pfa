using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    public class ProduitsApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProduitsApi
        [HttpGet]
        public IQueryable<Produit> GetProduits()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;

            return db.Produits;
        }

        [HttpGet]
        public List<Produit> GetProduits(String top)
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;

            List<Produit> myProduits = new List<Produit>();
            List<int> ids = db.Database.SqlQuery<int>("SELECT id from (SELECT TOP 10 l.id_produit as id, SUM(l.quantite) as qty FROM LignePaniers l, Commandes c WHERE c.id_panier = l.id_panier  GROUP BY l.id_produit ORDER BY qty DESC) tab;").ToList();

            foreach (int id in ids)
            {
                Produit produit = db.Produits.Find(id);
                myProduits.Add(produit);
            }
            

            return myProduits;
        }
        
        [HttpGet]
        public IQueryable<Produit> GetProduits(int id_cat)
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            return db.Produits.Where(x => x.id_categorie == id_cat);
        }

        // GET: api/ProduitsApi/5
        [ResponseType(typeof(Produit))]
        public IHttpActionResult GetProduit(int id)
        {
            Produit produit = db.Produits.Find(id);
            if (produit == null)
            {
                return NotFound();
            }

            return Ok(produit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProduitExists(int id)
        {
            return db.Produits.Count(e => e.id == id) > 0;
        }
    }

    class SendProduct{

    }

}