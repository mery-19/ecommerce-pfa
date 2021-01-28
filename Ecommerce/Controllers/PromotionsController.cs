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
    [Authorize(Roles = "Admin")]
    public class PromotionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Promotions
        public ActionResult Index()
        {
            return View(db.Promotions.ToList());
        }

        // GET: Promotions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            return View(promotion);
        }

        // GET: Promotions/Create
        public ActionResult Create()
        {


            ViewBag.Produits = new MultiSelectList(db.Produits, "id", "name");
            return View();
        }

        // POST: Promotions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                db.Promotions.Add(promotion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Produits = new MultiSelectList(db.Produits, "id", "name");

            return View(promotion);
        }

        [HttpPost]
        public ActionResult CreateTest(Promotion promotion, List<String> obj)
        {
            if (ModelState.IsValid)
            {
                db.Promotions.Add(promotion);
                 db.SaveChanges();
                int? id = promotion.id;
                if(id != null)
                {
                    foreach(var id_produit in obj)
                    {
                        int find_id = Convert.ToInt32(id_produit);
                        Produit produit = db.Produits.Where(x => x.id == find_id).FirstOrDefault();
                        produit.id_promotion = id;
                        db.Entry(produit).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                   
                }
                /*                return RedirectToAction("Index");
                */
                return Json(new { data = true });
            }

            ViewBag.Produits = new MultiSelectList(db.Produits, "id", "name");

            return View(promotion);
        }

        // GET: Promotions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Produits = new MultiSelectList(db.Produits, "id", "name");
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            return View(promotion);
        }

        // POST: Promotions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                promotion.date_modification = DateTime.Now;
                db.Entry(promotion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(promotion);
        }

        // GET: Promotions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            return View(promotion);
        }

        // POST: Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Promotion promotion = db.Promotions.Find(id);
            db.Promotions.Remove(promotion);
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
