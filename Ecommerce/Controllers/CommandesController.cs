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
    public class CommandesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Commandes
        public ActionResult Index()
        {
            var commandes = db.Commandes.Include(c => c.ModeLivraison).Include(c => c.ModePaiement).Include(c => c.Panier).Include(c => c.StatusCommande);
            return View(commandes.ToList());
        }

        // GET: Commandes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commande commande = db.Commandes.Find(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            return View(commande);
        }

        // GET: Commandes/Create
        public ActionResult Create()
        {
            ViewBag.id_mode_livraison = new SelectList(db.ModeLivraisons, "id", "libele");
            ViewBag.id_paiement = new SelectList(db.ModePaiements, "id", "libele");
            ViewBag.id_panier = new SelectList(db.Paniers, "id", "id_user");
            ViewBag.id_status = new SelectList(db.StatusCommandes, "id", "libele");
            return View();
        }

        // POST: Commandes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Commande commande)
        {
            if (ModelState.IsValid)
            {
                db.Commandes.Add(commande);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_mode_livraison = new SelectList(db.ModeLivraisons, "id", "libele", commande.id_mode_livraison);
            ViewBag.id_paiement = new SelectList(db.ModePaiements, "id", "libele", commande.id_paiement);
            ViewBag.id_panier = new SelectList(db.Paniers, "id", "id_user", commande.id_panier);
            ViewBag.id_status = new SelectList(db.StatusCommandes, "id", "libele", commande.id_status);
            return View(commande);
        }

        // GET: Commandes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commande commande = db.Commandes.Find(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_mode_livraison = new SelectList(db.ModeLivraisons, "id", "libele", commande.id_mode_livraison);
            ViewBag.id_paiement = new SelectList(db.ModePaiements, "id", "libele", commande.id_paiement);
            ViewBag.id_panier = new SelectList(db.Paniers, "id", "id_user", commande.id_panier);
            ViewBag.id_status = new SelectList(db.StatusCommandes, "id", "libele", commande.id_status);
            return View(commande);
        }

        // POST: Commandes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,description,date_ajout,id_panier,id_status,id_mode_livraison,id_paiement")] Commande commande)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commande).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_mode_livraison = new SelectList(db.ModeLivraisons, "id", "libele", commande.id_mode_livraison);
            ViewBag.id_paiement = new SelectList(db.ModePaiements, "id", "libele", commande.id_paiement);
            ViewBag.id_panier = new SelectList(db.Paniers, "id", "id_user", commande.id_panier);
            ViewBag.id_status = new SelectList(db.StatusCommandes, "id", "libele", commande.id_status);
            return View(commande);
        }

        // GET: Commandes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commande commande = db.Commandes.Find(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            return View(commande);
        }

        // POST: Commandes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Commande commande = db.Commandes.Find(id);
            db.Commandes.Remove(commande);
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
