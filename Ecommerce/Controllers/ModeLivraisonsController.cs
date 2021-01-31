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
    public class ModeLivraisonsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ModeLivraisons
        public ActionResult Index()
        {
            return View(db.ModeLivraisons.ToList());
        }

        // GET: ModeLivraisons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModeLivraison modeLivraison = db.ModeLivraisons.Find(id);
            if (modeLivraison == null)
            {
                return HttpNotFound();
            }
            return View(modeLivraison);
        }

        // GET: ModeLivraisons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModeLivraisons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,libele,frais_livraison,description")] ModeLivraison modeLivraison)
        {
            if (ModelState.IsValid)
            {
                db.ModeLivraisons.Add(modeLivraison);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modeLivraison);
        }

        // GET: ModeLivraisons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModeLivraison modeLivraison = db.ModeLivraisons.Find(id);
            if (modeLivraison == null)
            {
                return HttpNotFound();
            }
            return View(modeLivraison);
        }

        // POST: ModeLivraisons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,libele,frais_livraison,description")] ModeLivraison modeLivraison)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modeLivraison).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modeLivraison);
        }

        // GET: ModeLivraisons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModeLivraison modeLivraison = db.ModeLivraisons.Find(id);
            if (modeLivraison == null)
            {
                return HttpNotFound();
            }
            return View(modeLivraison);
        }

        // POST: ModeLivraisons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ModeLivraison modeLivraison = db.ModeLivraisons.Find(id);
            db.ModeLivraisons.Remove(modeLivraison);
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
