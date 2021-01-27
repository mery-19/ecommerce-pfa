using Ecommerce.Models;
using System;
using System.Collections.Generic;
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
            Produit produit = db.Produits.Find(id);

            if (id == null)
            {
                return HttpNotFound();
            }
            return View(produit);
        }
    }
}