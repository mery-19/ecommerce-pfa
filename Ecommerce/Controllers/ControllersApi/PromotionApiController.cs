using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Ecommerce.Controllers.ControllersApi
{
    public class PromotionApiController : ApiController
    {
        private  ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PromotionApi
        public IEnumerable<Promotion> Get()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            return db.Promotions;
        }

        // GET: api/PromotionApi/5
        [ResponseType(typeof(Promotion))]
        public IHttpActionResult Get(int id)
        {

            Promotion promo = db.Promotions.Find(id);
            if (promo == null)
            {
                return NotFound();
            }

            return Ok(promo);
        }

        // POST: api/PromotionApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/PromotionApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PromotionApi/5
        public void Delete(int id)
        {
        }
    }
}
