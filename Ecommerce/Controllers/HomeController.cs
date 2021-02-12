using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? page)
        {
            List<Produit> produits = db.Produits.ToList();
            List<ProduitDetails> myProduits = new List<ProduitDetails>();

            foreach (Produit produit in produits)
            {
                ProduitDetails produitDetails = new ProduitDetails();

                produitDetails.Produit = produit;
                produitDetails.real_price = produit.prix_vente + (produit.prix_vente * produit.tva) / 100;
                if (produit.id_promotion != null)
                {
                    Promotion p = db.Promotions.Find(produit.id_promotion);
                    produitDetails.save_price = (produitDetails.real_price * p.taux_promotion) / 100;
                    produitDetails.deal_price = produitDetails.real_price - produitDetails.save_price;
                }

                myProduits.Add(produitDetails);
            }
            return View(myProduits.ToPagedList(page ?? 1, 10));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}