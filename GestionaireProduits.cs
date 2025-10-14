namespace ProducInventory
{
    class GestionaireProduits
    {
        private List<Produit> Produits = [];

        private bool EstVide(bool showMessage = true)
        {
            if(showMessage &&!(Produits.Count > 0)) Console.WriteLine("La liste de produit est vide");
            return !(Produits.Count > 0);
        }

        public void AjouterProduit(Produit produit)
        {
            if (EstVide(false))
            {
                Produits.Add(produit);
                return;
            }
            
            if (RechercherProduit(produit.Reference) == null)
            {
                Produits.Add(produit);
                Console.WriteLine($"Le produit {produit.Nom} a été ajouté avec succes");
            }
            else
            {
                Console.WriteLine("Impossible d'ajouter ce produit, il existe déjà");
            }
        }

        public void AfficherTousProduits()
        {
            if (EstVide()) return;
            {
                foreach (var produit in Produits)
                {
                    Console.WriteLine(produit.ToString());
                }
            }
        }

        public Produit? RechercherProduit(string reference)
        {
            if (EstVide()) return null;

            var produit = Produits.FirstOrDefault(p => p.Reference == reference);

            if (produit != null)
            {
                return produit;
            }
            else
            {
                return null;
            }
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
                Produits.Remove(produitASupprimer);
                return true;
            }
            return false;
        }

        public void AfficherStatistiques()
        {
            if (EstVide()) return;

            Console.WriteLine($"Le nombre total de produit est {Produits.Count}");

            double valeurTotalInventaire = 0;

            foreach (var produit in Produits)
            {
                valeurTotalInventaire += produit.ValeurStock();
            }

            Console.WriteLine($"La valeur totale de l'inventaire est {valeurTotalInventaire}");

            var produitLePlusCher = Produits.OrderByDescending(p => p.Prix).FirstOrDefault();
            if (produitLePlusCher != null)
            {
                Console.WriteLine($"Le produit le plus cher est : {produitLePlusCher.Nom} avec un prix de {produitLePlusCher.Prix}");
            }
        }

        public void ChargerProduits(string cheminFichier)
        {
            try
            {
                string[] lignes = File.ReadAllLines(cheminFichier);

                foreach (var ligne in lignes)
                {
                    var listeProprietes = ligne.Split("|");
                    Produit produit = new(listeProprietes[0], listeProprietes[1], double.Parse(listeProprietes[2]), int.Parse(listeProprietes[3]), listeProprietes[4]);
                    AjouterProduit(produit);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Erreur: Le fichier n'a pas été trouvé.");
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Erreur d'entrée/sortie: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur: {ex.Message}");
            }

        }
        
        public void SauvegarderProduits(string cheminFichier)
        {
            if (EstVide()) return;
            try
            {
                using StreamWriter ecrivain = new StreamWriter(cheminFichier, true);
                
                foreach (var produit in Produits)
                {
                    ecrivain.WriteLine($"{produit.Nom}|{produit.Reference}|{produit.Prix}|{produit.Quantite}|{produit.Categorie}");
                }
                
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Erreur: Le fichier n'a pas été trouvé.");
                Produits = [];
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Erreur d'entrée/sortie: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur: {ex.Message}");
            }
        }

    }
    
    
}