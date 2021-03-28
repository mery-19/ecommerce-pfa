
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Ecommerce.Models;

namespace Ecommerce.Controllers.ControllersApi
{
    public class LoginApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        public LoginApiController()
        {
        }

        public LoginApiController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        [HttpPost]
         public async Task<ApplicationUser> login(LoginViewModel model)
        {
            
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            var user = UserManager.FindByEmail(model.Email);
            if(user != null)
            {
                ApplicationUser resultUser = await UserManager.FindAsync(user.UserName, model.Password);
                return (resultUser != null)? db.Users.Find(resultUser.Id):null;
            }
            else
            {
                return null;
            }
            
           
        }
    }
}
