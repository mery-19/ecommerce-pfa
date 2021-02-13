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
            List<Categorie> categories = db.Categories.ToList();
            ViewBag.categories = categories;
            return View(produits.ToPagedList(page ?? 1, 10));
        }

        [HttpPost]
        public JsonResult Index(string Prefix)
        {
            string[] produits = db.Produits.Where(x => x.name.Contains(Prefix)).Select(x => x.name).ToArray();
      
            return Json(produits, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Categorie(int? page,int id)
        {
            Categorie categorie = db.Categories.Find(id);
            ViewBag.cat_name = categorie.libele;
            List<Produit> produits = db.Produits.Where(x => x.id_categorie == id).ToList();
            return View(produits.ToPagedList(page ?? 1, 12));
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