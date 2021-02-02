﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    public class CommandesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Commandes
        public ActionResult Index()
        {
            ApplicationUser user = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            var commandes = db.Commandes.Where(x => x.Panier.User.UserName.Equals(user.UserName))
                .Include(c => c.ModeLivraison).Include(c => c.ModePaiement).Include(c => c.Panier).Include(c => c.StatusCommande);
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
            //GET id_user
            string name = System.Web.HttpContext.Current.User.Identity.Name;
            ApplicationUser user = db.Users.Where(x => x.UserName.Equals(name)).FirstOrDefault();

            //GET id_panier
            Panier panier = user.Paniers.Last();
            commande.id_panier = panier.id;

            //set id_status == En cours (par defaut)

            //CALCUL AND SET prix_ht; prix_tva, prix_total pour les produits dans la ligne de panier
            float prix_ht, prix_tva, prix_total;
            float prix_ht_commande = 0, prix_tva_commande = 0, prix_total_commande = 0;
            List<LignePanier> lignePaniers = new List<LignePanier>();
            lignePaniers = panier.LignePaniers.ToList();
            foreach (LignePanier ligne in lignePaniers)
            {
                if (ligne.Produit.Promotion != null)
                {
                    prix_ht = ligne.Produit.prix_vente * ligne.quantite;
                    prix_ht = prix_ht - (prix_ht * ligne.Produit.Promotion.taux_promotion) / 100;
                    prix_tva = (prix_ht * ligne.Produit.tva) / 100;
                    prix_total = prix_ht + prix_tva;
                }
                else
                {
                    prix_ht = ligne.Produit.prix_vente * ligne.quantite;
                    prix_tva = (prix_ht * ligne.Produit.tva) / 100;
                    prix_total = prix_ht + prix_tva;
                }


                prix_ht_commande += prix_ht;
                prix_tva_commande += prix_tva;
                prix_total_commande += prix_total;

                //update prix dans la table des ligne de panier
                ligne.prix_ht = prix_ht;
                ligne.prix_tva = prix_tva;
                ligne.prix_total = prix_total;
                db.Entry(ligne).State = EntityState.Modified;

                //Update quantite produit
            }

            //CALCUL AND SET prix_ht; prix_tva, prix_total pour les produits dans la ligne de panier
            commande.prix_ht = prix_ht_commande;
            commande.prix_tva = prix_tva_commande;
            commande.prix_total = prix_total_commande;

            db.Commandes.Add(commande);
            int res = db.SaveChanges();
            if (res != 0)
            {
                // add nouvau panier for this user
                Panier panier1 = new Panier();
                panier1.id_user = user.Id;
                db.Paniers.Add(panier1);
                db.SaveChanges();

                Session["coutPanier"] = 0;
            }

            return Redirect(Url.Action("Index", "LignePaniers"));
/*            return Redirect(Request.UrlReferrer.ToString());
*/        }


        [HttpPost]
/*        [ValidateAntiForgeryToken]
*/        public void showProductInCommande(int id)
        {
            Commande commande = db.Commandes.Find(id);
            List<LignePanier> lignes = db.LignePaniers.Where(x => x.id_panier == commande.id_panier).ToList();
            List<lesProduits> lesProduits = new List<lesProduits>();
            foreach (LignePanier l in lignes)
            {
                lesProduits my = new lesProduits();
                my.name = l.Produit.name;
                my.image = l.Produit.image;
                my.qty = l.quantite;
                my.prix = l.prix_total;
                lesProduits.Add(my);
            }
                JavaScriptSerializer js = new JavaScriptSerializer();

           
            Response.Write(js.Serialize(lesProduits));
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

class lesProduits
{
    public string name { get; set; }
    public string image { get; set; }
    public float qty { get; set; }
    public float? prix { get; set; }
}
