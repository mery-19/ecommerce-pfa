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

        // GET: LignePaniers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LignePanier lignePanier = db.LignePaniers.Find(id);
            if (lignePanier == null)
            {
                return HttpNotFound();
            }
            return View(lignePanier);
        }

        // GET: LignePaniers/Create
        public ActionResult Create()
        {
            ViewBag.id_panier = new SelectList(db.Paniers, "id", "id");
            ViewBag.id_produit = new SelectList(db.Produits, "id", "name");
            return View();
        }

        // POST: LignePaniers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_panier,id_produit,quantite,prix_total,tva")] LignePanier lignePanier)
        {
            if (ModelState.IsValid)
            {
                db.LignePaniers.Add(lignePanier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_panier = new SelectList(db.Paniers, "id", "id", lignePanier.id_panier);
            ViewBag.id_produit = new SelectList(db.Produits, "id", "name", lignePanier.id_produit);
            return View(lignePanier);
        }

        // GET: LignePaniers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LignePanier lignePanier = db.LignePaniers.Find(id);
            if (lignePanier == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_panier = new SelectList(db.Paniers, "id", "id", lignePanier.id_panier);
            ViewBag.id_produit = new SelectList(db.Produits, "id", "name", lignePanier.id_produit);
            return View(lignePanier);
        }

        // POST: LignePaniers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_panier,id_produit,quantite,prix_total,tva")] LignePanier lignePanier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lignePanier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_panier = new SelectList(db.Paniers, "id", "id", lignePanier.id_panier);
            ViewBag.id_produit = new SelectList(db.Produits, "id", "name", lignePanier.id_produit);
            return View(lignePanier);
        }

        // GET: LignePaniers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LignePanier lignePanier = db.LignePaniers.Find(id);
            if (lignePanier == null)
            {
                return HttpNotFound();
            }
            return View(lignePanier);
        }

        // POST: LignePaniers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LignePanier lignePanier = db.LignePaniers.Find(id);
            db.LignePaniers.Remove(lignePanier);
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
