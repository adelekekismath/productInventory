namespace ProducInventory
{
    class FichierTexteStockage: IStockageProduits
    {
        private static bool IsFileExist(string cheminFichier)
        {
            if (!File.Exists(cheminFichier))
            {
                Console.WriteLine("Le fichier spécifié n'existe pas.");
                return false;
            }
            return true;
        }
        public List<Produit> Charger(string cheminFichier)
        {
            List<Produit> produitsCharger = [];

            if (!IsFileExist(cheminFichier)) return produitsCharger;

            try
            {
                foreach (var ligne in File.ReadAllLines(cheminFichier))
                {
                    var champs = ligne.Split('|');
                    if (champs.Length >= 5)
                    {
                        if (double.TryParse(champs[2], out double prix) && int.TryParse(champs[3], out int quantite) && !champs.Any(c => string.IsNullOrWhiteSpace(c)))
                        {
                            produitsCharger.Add(new Produit(
                                champs[0],
                                champs[1],
                                prix,
                                quantite,
                                champs[4]
                            ));
                        }
                        else
                        {
                            Console.WriteLine($"Données invalides pour le produit : {ligne}");
                        }
                    }
                }
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Erreur d'entrée/sortie: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement du fichier : {ex.Message}");
                return [];
            }
            
            return produitsCharger;
        }

        public void Sauvegarder(string cheminFichier, List<Produit>? produits = null)
        {
            if (IsFileExist(cheminFichier) && produits != null && produits.Count > 0)
            {
                try
                {
                    using StreamWriter ecrivain = new(cheminFichier, false);

                    foreach (var produit in produits)
                    {
                        ecrivain.WriteLine($"{produit.Nom}|{produit.Reference}|{produit.Prix}|{produit.Quantite}|{produit.Categorie}");
                    }

                    Console.WriteLine("La sauvegarde a été éffectuée avec succès.");

                }
                catch (IOException ioEx)
                {
                    Console.WriteLine($"Erreur d'entrée/sortie: {ioEx.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la sauvegarde du fichier : {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Aucun produit à sauvegarder ou le fichier n'existe pas.");
            }
        }

    }
    
}