namespace ProducInventory {
    class Program {
        static GestionaireProduits StockProduits = new();
        const string cheminFichier = "Inventaire.txt";

        static int GetValidInt()
        {
            int number;
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine(" Veuillez rentrer un chiffre");
            }
            return number;
        }

        static double GetValidDouble()
        {
            double number;
            while (!double.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine(" Veuillez rentrer un chiffre");
            }
            return (double)number;
        }

        static string GetValidString(bool allowNumbers = false)
        {
            string input = Console.ReadLine() ?? "";
            while (string.IsNullOrWhiteSpace(input) || !allowNumbers && input.All(char.IsDigit))
            {
                Console.WriteLine(" Veuillez rentrer une chaîne de caractères valide");
                input = Console.ReadLine() ?? "";
            }
            return input;
        }

        static void AjouterUnProduit()
        {
            Console.WriteLine("Entrez le nom du produit:");
            string nom = GetValidString();
            Console.WriteLine("Entrez la référence du produit:");
            string reference = GetValidString(true);
            while (StockProduits.RechercherProduit(reference) != null)
            {
                Console.WriteLine(" Cette réference existe déjà, veuillez réessayer");
                reference = GetValidString(true);
            }
            Console.WriteLine("Entrez le prix du produit:");
            double prix = GetValidDouble();
            while (prix <= 0)
            {
                Console.WriteLine(" Veuillez rentrer un prix supérieur à 0");
                prix = GetValidDouble();
            }
            Console.WriteLine("Entrez la quantité du produit:");
            int quantite = GetValidInt();
            while (quantite < 0)
            {
                Console.WriteLine(" Veuillez rentrer une quantité supérieur ou égale à 0");
                quantite = GetValidInt();
            }
            Console.WriteLine("Entrez la catégorie du produit:");
            string categorie = GetValidString();

            Produit produit = new(nom, reference, prix, quantite, categorie);
            StockProduits.AjouterProduit(produit);
        }

        static void ModifierProduit()
        {
            Console.WriteLine("Entrez le nom du produit:");
            string nom = GetValidString();
            Console.WriteLine("Entrez la référence du produit:");
            string reference = GetValidString(true);
            while (StockProduits.RechercherProduit(reference) == null)
            {
                Console.WriteLine(" Ce produit n'existe pas, veuillez rentrer une réference valide");
                reference = GetValidString();
            }
            Console.WriteLine("Entrez le prix du produit:");
            double prix = GetValidDouble();
            while (prix <= 0)
            {
                Console.WriteLine(" Veuillez rentrer un prix supérieur à 0");
                prix = GetValidDouble();
            }
            Console.WriteLine("Entrez la quantité du produit:");
            int quantite = GetValidInt();
            while (quantite < 0)
            {
                Console.WriteLine(" Veuillez rentrer un prix supérieur ou égale à 0");
                prix = GetValidDouble();
            }
            Console.WriteLine("Entrez la catégorie du produit:");
            string categorie = GetValidString();

            Produit produit = new(nom, prix, quantite, categorie);

            StockProduits.ModifierProduit(reference, produit);

        }

        static void SupprimerProduit()
        {
            Console.WriteLine("Entrez la référence du produit:");
            string reference = GetValidString();

            Console.WriteLine("Etes vous sur de vouloir supprimer ce produit?");
            Console.WriteLine("Entrez 1 pour 'OUI' et un autre caratere quelconque pour 'NON'?");
            string confirmation = Console.ReadLine() ?? "";
            if (int.Parse(confirmation) == 1)
            {
                StockProduits.SupprimerProduit(reference);
            }

        }
        
        static void Sauvegarder(bool confirmationNeeded = true)
        {
            if (StockProduits.EstAJour(cheminFichier)) return;
            if (confirmationNeeded)
            {
                Console.WriteLine("\nVoulez-vous sauvegarder les changements?");
                Console.WriteLine("Entrez 1 pour 'OUI' et un autre caratere quelconque pour 'NON'?");
                int confirmationChoice ;
                if (int.TryParse(Console.ReadLine(), out confirmationChoice ) && confirmationChoice == 1)
                {
                    StockProduits.SauvegarderProduits(cheminFichier);
                }
            }
            else
            {
                StockProduits.SauvegarderProduits(cheminFichier);
                
            }
            
        }

        static void RechercherProduit()
        {
            string reference = GetValidString();
            var produit = StockProduits.RechercherProduit(reference);

            if (produit != null)
            {
                Console.WriteLine("\nVoici le produit recherché:\n");
                produit.ToString();
            }
            else
            {
                Console.WriteLine("\nCe produit n'existe pas\n");
            }

        }
        
        static void Quitter()
        {
            if (!StockProduits.EstAJour(cheminFichier)){
                Sauvegarder();
                Environment.Exit(1);
            }
            Environment.Exit(1);
        }
        
        static void AfficherLesChoix()
        {
            Console.WriteLine(@"
=== GESTIONNAIRE D'INVENTAIRE ===
1. Ajouter un produit
2. Afficher tous les produits
3. Rechercher un produit (par référence)
4. Modifier un produit
5. Supprimer un produit
6. Afficher les statistiques
7. Sauvegarder l'inventaire
8. Quitter
Votre choix :");
        }



        static void Main(string[] args) {
            Console.WriteLine("\nChargement de l'inventaire...\n");
            StockProduits.ChargerProduits(cheminFichier);
            AfficherLesChoix();
            int choix = GetValidInt();
            
            while(choix != 8)
            {

                switch (choix)
                {
                    case 1:
                        AjouterUnProduit();
                        Sauvegarder();
                        break;

                    case 2:
                        Console.WriteLine("\nVoici la liste des produits:\n");
                        StockProduits.AfficherTousProduits();
                        break;

                    case 3:
                        RechercherProduit();
                        break;

                    case 4:
                        ModifierProduit();
                        Sauvegarder();
                        break;

                    case 5:
                        SupprimerProduit();
                        Sauvegarder();
                        break;

                    case 6:
                        StockProduits.AfficherStatistiques();
                        break;

                    case 7:
                        Sauvegarder(false);
                        break;

                    case 8:
                        Quitter();
                        break;
                    default:
                        Console.WriteLine("Choix invalide");
                        break;
                }
                
                AfficherLesChoix();
                choix = GetValidInt();
             
            }
        }
    }
}