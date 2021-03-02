using Ecommerce.Models;
using Microsoft.AspNet.Identity;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
/*    [EnableCors(origins: "*", headers: "*", methods: "*")]
*/    public class PaymentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        ApplicationUser user = new ApplicationUser();

        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SuccessView()
        {
            return View();
        }

        public ActionResult FailureView()
        {
            return View();
        }

        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Payment/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                   /* string url = Url.Action(paypalRedirectUrl);

                    return new JsonResult()
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = new Dictionary<string, string>() { { "url", url } }
                    };*/
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }
            //on successful payment, show success page to user.  


            string name = System.Web.HttpContext.Current.User.Identity.Name;
            user = db.Users.Where(x => x.UserName.Equals(name)).FirstOrDefault(); Panier panier = user.Paniers.Last();

            Commande commande = new Commande();
            commande.address = user.Address;
            commande.phone = user.PhoneNumber;
            commande.id_mode_livraison = 6;
            commande.id_paiement = 2;

            CreateCommande(commande);

            return Redirect(Url.Action("Index", "LignePaniers"));
        }
        private PayPal.Api.Payment payment;

        public JsonRequestBehavior JsonRequestBehavior { get; private set; }

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        public Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            //create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };

            string name = System.Web.HttpContext.Current.User.Identity.Name;
            user = db.Users.Where(x => x.UserName.Equals(name)).FirstOrDefault(); Panier panier = user.Paniers.Last();

            var lignePaniers = db.LignePaniers.Where(x => x.id_panier == panier.id).Include(l => l.Panier).Include(l => l.Produit);
            decimal? price = 0;
            foreach (LignePanier ligne in lignePaniers)
            {
                float real_price = ligne.Produit.prix_vente + (ligne.Produit.prix_vente * ligne.Produit.tva) / 100;
                if (ligne.Produit.Promotion != null && ligne.Produit.Promotion.date_expiration > DateTime.Now)
                {
                    float save_price = (real_price * ligne.Produit.Promotion.taux_promotion) / 100;
                    real_price = real_price - save_price;
                }
                decimal m1 = Convert.ToDecimal(real_price);
                decimal d1 = Math.Round(m1, 2);
                itemList.items.Add(new Item()
                {
                    name = ligne.Produit.name,
                    currency = "USD",
                    price =  d1.ToString(),
                    quantity = ligne.quantite.ToString(),
                });
                price += (d1 * ligne.quantite);
            }

            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };

            decimal m = Convert.ToDecimal(price);
            decimal d = Math.Round(m, 2);

            //Final amount with details
            var amount = new Amount()
            {
                currency = "USD",
                total =Convert.ToString(d), 
            };

            var transactionList = new List<Transaction>();
            // Adding description about the transaction
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = itemList
            });


            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }


        public ActionResult CreateCommande(Commande commande)
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
                if (ligne.Produit.Promotion != null && ligne.Produit.Promotion.date_expiration > DateTime.Now)
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
                ligne.Produit.quantite_stock -= ligne.quantite;
                db.Entry(ligne.Produit).State = EntityState.Modified;
            }

            //CALCUL AND SET prix_ht; prix_tva, prix_total pour les produits dans la ligne de panier
            commande.prix_ht = prix_ht_commande;
            commande.prix_tva = prix_tva_commande;
            commande.prix_total = prix_total_commande;
            commande.address = user.Address;
            commande.phone = user.PhoneNumber;

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
            */

        }
        /*        ApplicationUser user = db.Users.Where(x => x.Id == User.Identity.GetUserId()).FirstOrDefault();
                Panier panier = user.Paniers.Last();
                var lignePaniers = db.LignePaniers.Where(x => x.id_panier == panier.id).Include(l => l.Panier).Include(l => l.Produit);
                int i = 0;
                    foreach(LignePanier ligne in lignePaniers)
                    {
                        itemList.items.Add(new Item()
                {
                    name = ligne.Produit.name,
                            currency = "DHs",
                            price = "1",
                            quantity = ligne.quantite.ToString(),
                        });
                        i++;
                    }*/

        /* private Payment CreatePayment(APIContext apiContext, string redirectUrl)
         {
             //create itemlist and add item objects to it  
             var itemList = new ItemList()
             {
                 items = new List<Item>()
             };
             //Adding Item Details like name, currency, price etc  
             itemList.items.Add(new Item()
             {
                 name = "Item Name comes here",
                 currency = "USD",
                 price = "1",
                 quantity = "1",
                 sku = "sku"
             });
             var payer = new Payer()
             {
                 payment_method = "paypal"
             };
             // Configure Redirect Urls here with RedirectUrls object  
             var redirUrls = new RedirectUrls()
             {
                 cancel_url = redirectUrl + "&Cancel=true",
                 return_url = redirectUrl
             };
             // Adding Tax, shipping and Subtotal details  
             var details = new Details()
             {
                 tax = "1",
                 shipping = "1",
                 subtotal = "1"
             };
             //Final amount with details  
             var amount = new Amount()
             {
                 currency = "USD",
                 total = "3", // Total must be equal to sum of tax, shipping and subtotal.  
                 details = details
             };
             var transactionList = new List<Transaction>();
             // Adding description about the transaction  
             transactionList.Add(new Transaction()
             {
                 description = "Transaction description",
                 invoice_number = "your generated invoice number", //Generate an Invoice No  
                 amount = amount,
                 item_list = itemList
             });
             this.payment = new Payment()
             {
                 intent = "sale",
                 payer = payer,
                 transactions = transactionList,
                 redirect_urls = redirUrls
             };
             // Create a payment using a APIContext  
             return this.payment.Create(apiContext);
         }*/
    }
}