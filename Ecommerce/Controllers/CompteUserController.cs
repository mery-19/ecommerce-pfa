using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class CompteUserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: CompteUser
        public ActionResult Index()
        {
            CompteUser compte = new CompteUser();
            ApplicationUser user = new ApplicationUser();
            string name = System.Web.HttpContext.Current.User.Identity.Name;
            user = db.Users.Where(x => x.UserName.Equals(name)).FirstOrDefault();
            compte.email = user.Email;
            compte.name = user.UserName;
            compte.address = user.Address;
            compte.phone = user.PhoneNumber;
            if (user.PasswordHash != null) compte.hasPassword = true;
            else compte.hasPassword = false;
            return View(compte);
        }

        // GET: CompteUser/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompteUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompteUser/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CompteUser/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CompteUser/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CompteUser/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompteUser/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
