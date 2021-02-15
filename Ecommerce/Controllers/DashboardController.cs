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
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Dashboard
        public ActionResult Index()
        {
            List<ApplicationUser> users = db.Users.ToList();
            List<Commande> commandes = db.Commandes.ToList();
            List<int> num_users = new List<int>();
            List<int> months = new List<int>();
            List<int> nb_commandes = new List<int>();
            List<int> nb_commandes_livre = new List<int>();
            List<int> nb_commandes_attente = new List<int>();

            /*--START-- select top 5 users*/
            List<TopUser> top_users = db.Database.SqlQuery<TopUser>("SELECT TOP 5 u.UserName name, SUM(c.prix_total) y " +
                                                            "FROM  Paniers p,Commandes c, [User] u " +
                                                            "WHERE  u.id = p.id_user and c.id_panier = p.id " +
                                                            "GROUP BY u.id,u.UserName " +
                                                            "ORDER BY SUM(c.prix_total) DESC;").ToList();
            /*--END-- select top 5 users*/
            
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
            ViewBag.top_users = top_users;
            ViewBag.nb_commandes = nb_commandes;
            ViewBag.nb_commandes_livre = nb_commandes_livre;
            ViewBag.nb_commandes_attente = nb_commandes_attente;
            return View();
        }

        public ActionResult Client()
        {
            return View();
        }
    }

    class TopUser
    {
        public string name { get; set; }
        public double y { get; set; }
    }
}