using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ecommerce.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Produit> Produits { get; set; }
        public virtual ICollection<Envies> Envies { get; set; }
        public virtual ICollection<Panier> Paniers { get; set; }
        public virtual ICollection<UserNotification> UserNotifications { get; set; }

        [Display(Name = "Adresse")]
        public string Address { get; set; }

        [Display(Name = "Image")]
        public string image { get; set; }

        [Display(Name = "date d'adhésion")]
        public DateTime date_ajout { get; set; } = DateTime.Now;

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
        }

        public virtual DbSet<BonAchat> BonAchats { get; set; }
        public virtual DbSet<Categorie> Categories { get; set; }
        public virtual DbSet<ModeLivraison> ModeLivraisons { get; set; }
        public virtual DbSet<ModePaiement> ModePaiements { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<StatusCommande> StatusCommandes { get; set; }
        public virtual DbSet<Produit> Produits { get; set; }
        public virtual DbSet<LignePanier> LignePaniers { get; set; }
        public virtual DbSet<Panier> Paniers { get; set; }
        public virtual DbSet<Commande> Commandes { get; set; }
        public virtual DbSet<Envies> Envies { get; set; }
        public virtual DbSet<AdminNotification> AdminNotifications { get; set; }
        public virtual DbSet<UserNotification> UserNotifications { get; set; }

        /*        public System.Data.Entity.DbSet<Ecommerce.Models.ApplicationUser> ApplicationUsers { get; set; }
        */
    }
}