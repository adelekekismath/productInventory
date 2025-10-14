namespace ProducInventory {
    class Program {
        static GestionaireProduits StockProduits = new();


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
            return number;
        }

        static string GetValidString()
        {
            string input = Console.ReadLine() ?? "";
            while (string.IsNullOrWhiteSpace(input))
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
            string reference = GetValidString();
            while (StockProduits.RechercherProduit(reference) != null)
            {
                Console.WriteLine(" Cette réference existe déjà");
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

            Produit produit = new(nom, reference, prix, quantite, categorie);
            StockProduits.AjouterProduit(produit);
        }

        static void ModifierProduit()
        {
            Console.WriteLine("Entrez le nom du produit:");
            string nom = GetValidString();
            Console.WriteLine("Entrez la référence du produit:");
            string reference = GetValidString();
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
        
        static void AfficherLesChoix()
        {
            Console.WriteLine(@"=== GESTIONNAIRE D'INVENTAIRE ===
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
            AfficherLesChoix();
            int choix = GetValidInt();
            StockProduits.ChargerProduits("Inventaire.txt");
            
            while(choix != 8)
            {

                switch (choix)
                {
                    case 1:
                        AjouterUnProduit();
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
                        break;

                    case 5:
                        SupprimerProduit();
                        break;

                    case 6:
                        SupprimerProduit();
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