using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Ecommerce.Controllers
{
    public class ProduitDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ProduitDetails/id
        public ActionResult Index(int? id, int? page)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            /* --START-- get information of produit */
            Produit produit = db.Produits.Find(id);
            /* --END-- get information of produit */

            int qty = produit.quantite_stock;
            int max_qty = (qty <= 5) ? qty : 5;
            List<int> qtys = new List<int>();
            for(int i=1; i<=max_qty; i++)
            {
                qtys.Add(i);
            }
            if(qtys.Count != 0)
            ViewBag.qty = new SelectList(qtys);
            
            /* --START-- See if the user connected and has the product in Panier */
            ApplicationUser user = new ApplicationUser();
            string name = System.Web.HttpContext.Current.User.Identity.Name;
            user = db.Users.Where(x => x.UserName.Equals(name)).FirstOrDefault();
            if (user != null)
            {
                Panier panier = user.Paniers.Last();
                LignePanier exist = db.LignePaniers.Find(panier.id, produit.id);
                if(exist != null){
                    if (qtys.Count != 0)
                    ViewBag.qty = new SelectList(qtys, qtys.Where(x=> x == exist.quantite).First());
                }
            }
            /* --END-- See if the user connected and has the product in Panier */
            List<Produit> produits = db.Produits.ToList();

            ViewBag.produits = allProducs();
            ViewBag.commentaires = Commentaires(id,page);
            return View(produit);
        }


        [HttpPost]
        public ActionResult addProduit(string qty, string id_produit)
        {
            //if id_user is null, message de authentifier
            ApplicationUser user = new ApplicationUser();
            string name = System.Web.HttpContext.Current.User.Identity.Name;
            user = db.Users.Where(x => x.UserName.Equals(name)).FirstOrDefault();
            if(user != null)
            {
                Panier panier = user.Paniers.Last();

                Produit produit = db.Produits.Find(Convert.ToInt32(id_produit));
                LignePanier ligne = new LignePanier();
                ligne.id_panier = panier.id;
                ligne.id_produit = Convert.ToInt32(id_produit);
                ligne.quantite = Convert.ToInt32(qty);
                LignePanier exist = db.LignePaniers.Find(ligne.id_panier, ligne.id_produit);
                if (exist == null)
                {
                    db.LignePaniers.Add(ligne);
                }else
                {
                    exist.quantite = ligne.quantite;
                    db.Entry(exist).State = EntityState.Modified;
                }
                int res = db.SaveChanges();

                if(res != 0)
                {
                    return Json(new { success = true, responseText = "Added successfully." }, JsonRequestBehavior.AllowGet);
                } else
                {
                    return Json(new { success = false, responseText = "Data not added" }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { success = false, responseText = "Please register." }, JsonRequestBehavior.AllowGet);

            }
            //insert id_cart, id_produit qty prices to database
        }

        public List<Produit> allProducs()
        {
            List<Produit> myProduits = new List<Produit>();
            List<int> ids = db.Database.SqlQuery<int>("SELECT id from (SELECT TOP 10 l.id_produit as id, SUM(l.quantite) as qty FROM LignePaniers l, Commandes c WHERE c.id_panier = l.id_panier  GROUP BY l.id_produit ORDER BY qty DESC) tab;").ToList();

            foreach(int id in ids)
            {
                Produit produit = db.Produits.Find(id);
                myProduits.Add(produit);
            }

            
            return myProduits;
        }


        [HttpPost]
        public ActionResult AddComment(int id_produit, string commentaire)
        {
            ApplicationUser user = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
 
            //select id_panier from the commendes
            List<LignePanier> lignes = db.LignePaniers.Where(x => x.id_produit == id_produit && x.Panier.id_user == user.Id).ToList();
            if(lignes.Count != 0)
            {
                foreach(LignePanier ligne in lignes)
                {
                    Commande commande = db.Commandes.Where(x => x.id_panier == ligne.Panier.id).FirstOrDefault();
                    if(commande != null)
                    {
                        Commentaire c = new Commentaire();
                        c.id_user = user.Id;
                        c.id_produit = id_produit;
                        c.commentaire = commentaire;
                        db.Commentaires.Add(c);
                        db.SaveChanges();
                        return Json(new { success = true, responseText = "Commentaire ajouté avec succés." }, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            return Json(new { success = false, responseText = "Vous ne pouvez pas commenter sur ce produit, car vous ne l'avez pas acheté." }, JsonRequestBehavior.AllowGet);
        }

        public PagedList.IPagedList<Commentaire> Commentaires(int? id, int? page)
        {
            PagedList.IPagedList<Commentaire> commentaires =db.Commentaires.Where(x => x.id_produit == id).OrderByDescending(x => x.id).ToList().ToPagedList(page ?? 1, 3);
            return commentaires;
        }
    }
}