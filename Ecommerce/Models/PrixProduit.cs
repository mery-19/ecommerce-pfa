using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class PrixProduit
    {
        public PrixProduit(LignePanier lignePanier)
        {
            prix_unitaire = lignePanier.Produit.prix_vente + (lignePanier.Produit.prix_vente * lignePanier.Produit.tva) / 100;
            prix_total = prix_unitaire * lignePanier.quantite;
            economie = 0;
            economie_total = 0;

            if (lignePanier.Produit.Promotion != null)
            {
                economie = (prix_unitaire * lignePanier.Produit.Promotion.taux_promotion) / 100;
                economie_total = economie * lignePanier.quantite;
                prix_unitaire = prix_unitaire - economie;
                prix_total = prix_total - economie_total;
            }
        }

        public float prix_unitaire { get; set; }
        public float prix_total { get; set; }
        public float economie { get; set; }
        public float economie_total { get; set; }
    }
}