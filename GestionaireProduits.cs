namespace ProducInventory
{
    class GestionaireProduits
    {
        private List<Produit> Produits = [];

        private bool EstVide(bool showMessage = true)
        {
            if (showMessage && !(Produits.Count > 0)) Console.WriteLine("La liste de produit est vide");
            return !(Produits.Count > 0);
        }

        public void AjouterProduit(Produit produit)
        {
            if (EstVide(false))
            {
                Produits.Add(produit);
                return;
            }

            if (produit.Reference != null && RechercherProduit(produit.Reference) == null)
            {
                Produits.Add(produit);
                Console.WriteLine($"\nLe produit {produit.Nom} a été ajouté avec succes");
            }
            else if (produit.Reference == null)
            {
                Console.WriteLine("Impossible d'ajouter ce produit, la référence est nulle");
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

            Console.WriteLine("\nStatistiques de l'inventaire:\n");

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

        private bool isFileExist(string cheminFichier)
        {
            if (!File.Exists(cheminFichier))
            {
                Console.WriteLine("Le fichier spécifié n'existe pas.");
                return false;
            }
            return true;
        }

        public void ChargerProduits(string cheminFichier)
        {
            try
            {

                foreach (var ligne in File.ReadAllLines(cheminFichier))
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
                using StreamWriter ecrivain = new StreamWriter(cheminFichier, false);

                foreach (var produit in Produits)
                {
                    ecrivain.WriteLine($"{produit.Nom}|{produit.Reference}|{produit.Prix}|{produit.Quantite}|{produit.Categorie}");
                }

                Console.WriteLine("La sauvegarde a été éffectuée avec succès.");

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
    
    
        public bool EstAJour(string cheminFichier)
        {
            if (!isFileExist(cheminFichier)) return false;

            List<Produit> produitsFichier = [];

            try
            {
                foreach (var ligne in File.ReadAllLines(cheminFichier))
                {
                    var champs = ligne.Split('|');
                    if (champs.Length >= 5)
                    {
                        produitsFichier.Add(new Produit(
                            champs[0],
                            champs[1],
                            double.Parse(champs[2]),
                            int.Parse(champs[3]),
                            champs[4]
                        ));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement du fichier : {ex.Message}");
                return false;
            }

            if (Produits.Count != produitsFichier.Count) return false;

            return Produits.OrderBy(p => p.Reference).SequenceEqual(produitsFichier.OrderBy(p => p.Reference));

        }
    }
    
    
}