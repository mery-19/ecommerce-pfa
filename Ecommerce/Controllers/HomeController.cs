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
            
            Top();
            return View(produits.ToPagedList(page ?? 1, 10));
        }

        [HttpPost]
        public JsonResult Index(string Prefix)
        {
            string[] produits = db.Produits.Where(x => x.name.Contains(Prefix)).Select(x => x.name).ToArray();
            string[] categories = db.Categories.Where(x => x.libele.Contains(Prefix)).Select(x => x.libele).ToArray();
            IEnumerable<string> result = produits.Concat(categories);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Categorie(int? page,int id)
        {
            Categorie categorie = db.Categories.Find(id);
            ViewBag.cat_name = categorie.libele;
            List<Produit> produits = db.Produits.Where(x => x.id_categorie == id).ToList();
            return View(produits.ToPagedList(page ?? 1, 12));
        }

        public ActionResult Search(int? page,string txt)
        {
            ViewBag.txt = txt;
            List<Produit> produits = db.Produits.Where(x => x.name.Contains(txt) || x.Categorie.libele.Contains(txt)).ToList();
            return View(produits.ToPagedList(page ?? 1, 10));
        }

        public ActionResult Top()
        {
            List<Produit> myProduits = new List<Produit>();
            List<int> ids = db.Database.SqlQuery<int>("SELECT id from (SELECT TOP 10 l.id_produit as id, SUM(l.quantite) as qty FROM LignePaniers l, Commandes c WHERE c.id_panier = l.id_panier  GROUP BY l.id_produit ORDER BY qty DESC) tab;").ToList();

            foreach (int id in ids)
            {
                Produit produit = db.Produits.Find(id);
                myProduits.Add(produit);
            }
            ViewBag.produits = myProduits;
            return PartialView(myProduits);
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