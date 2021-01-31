using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    public class LignePaniersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LignePaniers
        public ActionResult Index()
        {
            ApplicationUser user = new ApplicationUser();
            string name = System.Web.HttpContext.Current.User.Identity.Name;
            user = db.Users.Where(x => x.UserName.Equals(name)).FirstOrDefault();
            user = db.Users.Where(x => x.UserName.Equals(name)).FirstOrDefault();
            if (user != null)
            {
                Panier panier = user.Paniers.Last();
                var lignePaniers = db.LignePaniers.Where(x => x.id_panier == panier.id).Include(l => l.Panier).Include(l => l.Produit);
                return View(lignePaniers.ToList());
            }
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleleLignePanier(int id_produit,int id_panier)
        {

            LignePanier lignePanier = db.LignePaniers.Where(x=> x.id_panier == id_panier && x.id_produit == id_produit).FirstOrDefault();
            db.LignePaniers.Remove(lignePanier);
            int res = db.SaveChanges();

            if (res != 0)
            {
                return Json(new { success = true, responseText = "Deletes successfully." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, responseText = "deleted error" }, JsonRequestBehavior.AllowGet);
            }
        }

        //GET finaliser la commande
        public ActionResult FinaliserCommande()
        {
            //send mode de paiment
            List<ModePaiement> modesP = new List<ModePaiement>();
            modesP = db.ModePaiements.ToList();
            ViewBag.modesPaiement = modesP;

            //sant mode de livraison
            List<ModeLivraison> modesL = new List<ModeLivraison>();
            modesL = db.ModeLivraisons.ToList();
            ViewBag.modesLivraisons = modesL;

            return View();

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
