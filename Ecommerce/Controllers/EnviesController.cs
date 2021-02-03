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

    public class EnviesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Envies
        public ActionResult Index()
        {
            ApplicationUser user = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();

            var envies = db.Envies.Where(x => x.id_user == user.Id).Include(e => e.Produit).Include(e => e.User);
            return View(envies.ToList());
        }

        [ValidateAntiForgeryToken]
        public ActionResult Add(int id_produit)
        {
            ApplicationUser user = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            Envies exist = new Envies();
            exist = db.Envies.Find(user.Id, id_produit);
            if(exist != null )
                return Json(new { success = true, responseText = "Vous avez déja ce produit dans vos liste d'envies." }, JsonRequestBehavior.AllowGet);

            Envies envie = new Envies();
            envie.id_produit = id_produit;
            envie.id_user = user.Id;
            db.Envies.Add(envie);
            int res = db.SaveChanges();
            if (res != 0)
            {
                return Json(new { success = true, responseText = "Ajouter au liste des envies avec succée." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, responseText = "Erreur d'ajout le produit." }, JsonRequestBehavior.AllowGet);
            }
        }


        // POST: Envies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Envies envies = db.Envies.Where(x => x.id == id).FirstOrDefault();
            db.Envies.Remove(envies);
            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
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
