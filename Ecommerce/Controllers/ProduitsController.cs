using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProduitsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Produits
        public ActionResult Index()
        {
            var produits = db.Produits.Include(p => p.Categorie).Include(p => p.Promotion).Include(p => p.User);
            return View(produits.ToList());
        }

        // GET: Produits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produit produit = db.Produits.Find(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            return View(produit);
        }

        // GET: Produits/Create
        public ActionResult Create()
        {
            ViewBag.id_categorie = new SelectList(db.Categories, "id", "libele");
            ViewBag.id_promotion = new SelectList(db.Promotions, "id", "libele");
            ViewBag.id_user = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Produits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Produit produit,HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                string name =  System.Web.HttpContext.Current.User.Identity.Name;
                ApplicationUser user = db.Users.Where(x => x.UserName.Equals(name)).FirstOrDefault();
                produit.id_user = user.Id;
                produit.date_ajout = DateTime.Now;
                string path = Path.Combine(Server.MapPath("~/Uploads/Produit_image"), image.FileName);
                image.SaveAs(path);
                produit.image = image.FileName;
                db.Produits.Add(produit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_categorie = new SelectList(db.Categories, "id", "libele", produit.id_categorie);
            ViewBag.id_promotion = new SelectList(db.Promotions, "id", "libele", produit.id_promotion);
            ViewBag.id_user = new SelectList(db.Users, "Id", "Email", produit.id_user);
            return View(produit);
        }

        // GET: Produits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produit produit = db.Produits.Find(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_categorie = new SelectList(db.Categories, "id", "libele", produit.id_categorie);
            ViewBag.id_promotion = new SelectList(db.Promotions, "id", "libele", produit.id_promotion);
            ViewBag.id_user = new SelectList(db.Users, "Id", "Email", produit.id_user);
            return View(produit);
        }

        // POST: Produits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Produit produit, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if(image != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Uploads/Produit_image"), image.FileName);
                    image.SaveAs(path);
                    produit.image = image.FileName;
                }
                
                produit.date_modification = DateTime.Now;
                db.Entry(produit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_categorie = new SelectList(db.Categories, "id", "libele", produit.id_categorie);
            ViewBag.id_promotion = new SelectList(db.Promotions, "id", "libele", produit.id_promotion);
            ViewBag.id_user = new SelectList(db.Users, "Id", "Email", produit.id_user);
            return View(produit);
        }

        // GET: Produits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produit produit = db.Produits.Find(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            return View(produit);
        }

        // POST: Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         public ActionResult DeleteConfirmed(int id)
        {
            Produit produit = db.Produits.Find(id);
            db.Produits.Remove(produit);
            db.SaveChanges();
            return RedirectToAction("Index");
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
