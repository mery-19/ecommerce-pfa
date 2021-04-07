using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ecommerce.Controllers.ControllersApi
{
    public class NotifictionsApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/NotifictionsApi
        public IEnumerable<Commande> Get(string id_user)
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            float notif_num = 0;

            /*PageSize: Number of items*/
            var commandes = db.Commandes.Where(x => x.Panier.id_user.Equals(id_user) && x.id_status == 2);

            /*--START-- to know the num of notifications and restart not*/
            UserNotification userNotification = db.UserNotifications.Where(x => x.id_user == id_user).FirstOrDefault();
            notif_num = userNotification.num;
            userNotification.num = 0;
            db.Entry(userNotification).State = EntityState.Modified;
            db.SaveChanges();
            /*--END-- to know the num of notifications and restart not*/
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;

            return commandes.OrderByDescending(x => x.id);
        }

        // GET: api/NotifictionsApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/NotifictionsApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/NotifictionsApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/NotifictionsApi/5
        public void Delete(int id)
        {
        }
    }
}
