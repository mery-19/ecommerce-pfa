using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        static ApplicationDbContext db = new ApplicationDbContext();
        List<ApplicationUser> users = db.Users.ToList();
        List<Commande> commandes = db.Commandes.ToList();


        // GET: Dashboard
        public ActionResult Index(int? mois)
        {
            if (mois == null)
                mois = DateTime.Now.Month;

            ViewBag.mois = mois;
            List<int> num_users = new List<int>();
            List<int> months = new List<int>();
            List<int> nb_commandes = new List<int>();
            List<int> nb_commandes_livre = new List<int>();
            List<int> nb_commandes_attente = new List<int>();

           

            for (int i = 1; i <= 12; i++)
            {
                num_users.Add(users.Count(x => x.date_ajout.Year == DateTime.Now.Year && x.date_ajout.Month == i));
                nb_commandes.Add(commandes.Count(x => x.date_ajout.Year == DateTime.Now.Year && x.date_ajout.Month == i));
                nb_commandes_livre.Add(commandes.Count(x => x.date_update.Month == i && x.date_update.Year == DateTime.Now.Year && x.id_status ==2));
                nb_commandes_attente.Add(commandes.Count(x => x.date_ajout.Month == i && x.date_ajout.Year == DateTime.Now.Year && x.id_status==1));
                months.Add(i);
            }
            ViewBag.months = months;
            ViewBag.users = num_users;
            ViewBag.nb_commandes = nb_commandes;
            ViewBag.nb_commandes_livre = nb_commandes_livre;
            ViewBag.nb_commandes_attente = nb_commandes_attente;

            statisticsOfTheMonth(mois);
            statisticsOfTop();

            return View();
        }

        public ActionResult Client()
        {
            return View();
        }

        void statisticsOfTheMonth(int? mois)
        {
            int nb_clients_joins = db.Users.Count(x => x.date_ajout.Year == DateTime.Now.Year && x.date_ajout.Month == mois);
            int nb_commande_livre = db.Commandes.Count(x => x.date_ajout.Year == DateTime.Now.Year && x.date_ajout.Month == mois);
            List<Commande> cm = db.Commandes.Where(x => x.date_ajout.Year == DateTime.Now.Year && x.date_ajout.Month == mois && x.id_status == 2).ToList();
            float CA;
            if(cm != null)
            {
                CA = cm.Sum(x => x.prix_total);
            }else
            {
                CA = 0;
            }

            ViewBag.nb_clients_joins = nb_clients_joins;
            ViewBag.nb_commande_livre = nb_commande_livre;
            ViewBag.CA = CA;
        }

        void statisticsOfTop()
        {
            /*--START-- select top 5 users*/
            /*We select the users that paid more*/

            List<TopUser> top_users = db.Database.SqlQuery<TopUser>("SELECT TOP 5 u.UserName name, SUM(c.prix_total) y " +
                                                            "FROM  Paniers p,Commandes c, [User] u " +
                                                            "WHERE  u.id = p.id_user and c.id_panier = p.id " +
                                                            "GROUP BY u.id,u.UserName " +
                                                            "ORDER BY SUM(c.prix_total) DESC;").ToList();
            /*--END-- select top 5 users*/

            /*--START-- select top 5 Produits*/
            //we select the product that the quantite commandes is the greather

            List<TopProduct> top_products = db.Database.SqlQuery<TopProduct>("SELECT TOP 5 lp.id_produit id,pr.name name, SUM(lp.quantite) y " +
                "FROM LignePaniers lp, Commandes c, Paniers p,Produits pr " +
                "WHERE c.id_panier = p.id and lp.id_panier = p.id and pr.id = lp.id_produit " +
                "GROUP BY lp.id_produit, pr.name " +
                "ORDER BY y DESC;").ToList();
            /*--END-- select top 5 Produits*/

            ViewBag.top_users = top_users;
            ViewBag.top_products = top_products;

        }
    }

    class TopUser
    {
        public string name { get; set; }
        public double y { get; set; }
    }

    class TopProduct
    {
        int id;
        public string name { get; set; }
        public int y { get; set; }
    }
}