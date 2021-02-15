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
            List<int> num_users = new List<int>();
            List<int> months = new List<int>();

            

            for (int i = 1; i <= 12; i++)
            {
                num_users.Add(users.Count(x => x.date_ajout.Year == DateTime.Now.Year && x.date_ajout.Month == i));
                months.Add(i);
            }
            ViewBag.months = months;
            ViewBag.users = num_users;
            return View();
        }

        public ActionResult Client()
        {
            return View();
        }
    }
}