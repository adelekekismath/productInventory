namespace ProducInventory
{
    class GestionaireProduits( IStockageProduits stockage)
    {
        private List<Produit> _Produits = [];
        private readonly IStockageProduits _stockage = stockage;

        private bool EstVide(bool showMessage = true)
        {
            if (showMessage && _Produits.Count == 0) Console.WriteLine("La liste de produit est vide");
            return _Produits.Count == 0;
        }

        public void AjouterProduit(Produit produit)
        {
            if (EstVide(false))
            {
                _Produits.Add(produit);
                return;
            }

            if (produit.Reference != null && RechercherProduit(produit.Reference) == null)
            {
                _Produits.Add(produit);
                Console.WriteLine($"\nLe produit {produit.Nom} a été ajouté avec succes");
            }
            else
            {
                Console.WriteLine("Impossible d'ajouter ce produit, il existe déjà");
            }
        }

        public void AfficherTousProduits()
        {
            if (EstVide()) return;

            Console.WriteLine("\nListe des Produits dans l'inventaire:\n");
            foreach (var produit in _Produits) Console.WriteLine(produit.ToString());
        }

        public Produit? RechercherProduit(string reference)
        {
            if (EstVide()) return null;

            var produit = _Produits.FirstOrDefault(p => p.Reference == reference);

            return produit;
        }

        public bool ModifierProduit(string reference, Produit nouveauProduit)
        {
            if (EstVide()) return false;

            var produitAModifier = RechercherProduit(reference);
            if (produitAModifier != null)
            {
                produitAModifier.Nom = nouveauProduit.Nom;
                produitAModifier.Prix = nouveauProduit.Prix;
                produitAModifier.Categorie = nouveauProduit.Categorie;
                produitAModifier.Quantite = nouveauProduit.Quantite;

                return true;
            }
            return false;
        }

        public bool SupprimerProduit(string reference)
        {
            if (EstVide()) return false;

            var produitASupprimer = RechercherProduit(reference);
            if (produitASupprimer != null)
            {
                _Produits.Remove(produitASupprimer);
                return true;
            }
            return false;
        }


        public void AfficherStatistiques()
        {
            if (EstVide()) return;

            Console.WriteLine("\nStatistiques de l'inventaire:\n");

            Console.WriteLine($"Le nombre total de produit est {_Produits.Count}");

            double valeurTotalInventaire = 0;

            foreach (var produit in _Produits)
            {
                valeurTotalInventaire += produit.ValeurStock();
            }

            Console.WriteLine($"La valeur totale de l'inventaire est {valeurTotalInventaire:F2}");

            var produitLePlusCher = _Produits.OrderByDescending(p => p.Prix).FirstOrDefault();
            if (produitLePlusCher != null)
            {
                Console.WriteLine($"Le produit le plus cher est : {produitLePlusCher.Nom} avec un prix de {produitLePlusCher.Prix:F2}");
            }
        }

        public void ChargerProduits(string cheminFichier)
        {
            _Produits = _stockage.Charger(cheminFichier);
            if (!EstVide(false))
            {
                Console.WriteLine("Les produits ont été chargés avec succès depuis le fichier.");
            }

        }

        public void SauvegarderProduits(string cheminFichier)
        {
            if (EstVide()) return;
            _stockage.Sauvegarder(cheminFichier, _Produits);
        }
    
    
        public bool EstAJour(string cheminFichier)
        {
            List<Produit> produitsFichier = _stockage.Charger(cheminFichier);

            if (_Produits.Count != produitsFichier.Count) return false;

            return _Produits.OrderBy(p => p.Reference).SequenceEqual(produitsFichier.OrderBy(p => p.Reference));
        }
    }
    
    
}