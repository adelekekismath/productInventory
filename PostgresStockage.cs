using Npgsql;

namespace ProducInventory
{
    class PostgresStockage : IStockageProduits
    {
        private readonly string _connectionString;

        public PostgresStockage(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Produit> Charger()
        {
            List<Produit> produits = [];

            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                connection.Open();

                string query = "SELECT nom, reference, prix, quantite, categorie FROM produit";
                using var cmd = new NpgsqlCommand(query, connection);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    produits.Add(new Produit(
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetDouble(2),
                        reader.GetInt32(3),
                        reader.GetString(4)
                    ));
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement depuis PostgreSQL : {ex.Message}");
            }

            return produits;
        }

        public void Sauvegarder(List<Produit>? produits = null)
        {
            if (produits == null || produits.Count == 0)
            {
                Console.WriteLine("Aucun produit à sauvegarder.");
                return;
            }
            
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                connection.Open();

                foreach (var produit in produits)
                {
                    string insertQuery = @"
                        INSERT INTO produit (nom, reference, prix, quantite, categorie)
                        VALUES (@nom, @reference, @prix, @quantite, @categorie)
                        ON CONFLICT (reference) DO UPDATE
                        SET prix = EXCLUDED.prix,
                            quantite = EXCLUDED.quantite,
                            categorie = EXCLUDED.categorie;";

                    using var cmd = new NpgsqlCommand(insertQuery, connection);
                    cmd.Parameters.AddWithValue("@nom", produit.Nom);
                    cmd.Parameters.AddWithValue("@reference", produit.Reference != null ? produit.Reference : DBNull.Value);
                    cmd.Parameters.AddWithValue("@prix", produit.Prix);
                    cmd.Parameters.AddWithValue("@quantite", produit.Quantite);
                    cmd.Parameters.AddWithValue("@categorie", produit.Categorie);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la sauvegarde dans PostgreSQL : {ex.Message}");
            }
        }
    }
}