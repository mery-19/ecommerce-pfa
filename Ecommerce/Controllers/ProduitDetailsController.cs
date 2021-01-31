using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class ProduitDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ProduitDetails/id
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            /* --START-- get information of produit */
            Produit produit = db.Produits.Find(id);
            ProduitDetails produitDetails = new ProduitDetails();
            produitDetails.Produit = produit;
            produitDetails.real_price = produit.prix_vente + (produit.prix_vente * produit.tva) / 100;
            if(produit.Promotion != null)
            {
                produitDetails.save_price = (produitDetails.real_price * produit.Promotion.taux_promotion) / 100;
                produitDetails.deal_price = produitDetails.real_price - produitDetails.save_price;
            }
            /* --END-- get information of produit */

            int qty = produit.quantite_stock;
            int max_qty = (qty <= 5) ? qty : 5;
            List<int> qtys = new List<int>();
            for(int i=1; i<=max_qty; i++)
            {
                qtys.Add(i);
            }
            if(qtys.Count != 0)
            ViewBag.qty = new SelectList(qtys);
            
            /* --START-- See if the user connected and has the product in Panier */
            ApplicationUser user = new ApplicationUser();
            string name = System.Web.HttpContext.Current.User.Identity.Name;
            user = db.Users.Where(x => x.UserName.Equals(name)).FirstOrDefault();
            if (user != null)
            {
                Panier panier = user.Paniers.Last();
                LignePanier exist = db.LignePaniers.Find(panier.id, produit.id);
                if(exist != null){
                    if (qtys.Count != 0)
                    ViewBag.qty = new SelectList(qtys, qtys.Where(x=> x == exist.quantite).First());
                }
            }
            /* --END-- See if the user connected and has the product in Panier */

            return View(produitDetails);
        }


        [HttpPost]
        public ActionResult addProduit(string qty, string id_produit)
        {
            //if id_user is null, message de authentifier
            ApplicationUser user = new ApplicationUser();
            string name = System.Web.HttpContext.Current.User.Identity.Name;
            user = db.Users.Where(x => x.UserName.Equals(name)).FirstOrDefault();
            if(user != null)
            {
                Panier panier = user.Paniers.Last();

                Produit produit = db.Produits.Find(Convert.ToInt32(id_produit));
                LignePanier ligne = new LignePanier();
                ligne.id_panier = panier.id;
                ligne.id_produit = Convert.ToInt32(id_produit);
                ligne.quantite = Convert.ToInt32(qty);
                LignePanier exist = db.LignePaniers.Find(ligne.id_panier, ligne.id_produit);
                if (exist == null)
                {
                    db.LignePaniers.Add(ligne);
                }else
                {
                    exist.quantite = ligne.quantite;
                    db.Entry(exist).State = EntityState.Modified;
                }
                int res = db.SaveChanges();

                if(res != 0)
                {
                    return Json(new { success = true, responseText = "Added successfully." }, JsonRequestBehavior.AllowGet);
                } else
                {
                    return Json(new { success = false, responseText = "Data not added" }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { success = false, responseText = "Please register." }, JsonRequestBehavior.AllowGet);

            }
            //insert id_cart, id_produit qty prices to database
        }
    }
}