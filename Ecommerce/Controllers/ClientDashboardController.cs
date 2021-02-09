using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;


namespace Ecommerce.Controllers
{
    [Authorize(Roles = "User")]
    public class ClientDashboardController : Controller
    {

        // GET: ClientDashboard
        public ActionResult Index()
        {
  
            return View();
        }
    }
}