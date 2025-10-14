namespace ProducInventory
{
    class Produit {
        public string Nom { get; set; }
        public string? Reference { get; set; }
        public double Prix { get; set; }
        public int Quantite { get; set; }
        public string Categorie { get; set; }

        public Produit(string nom, string reference, double prix, int quantite, string categorie)
        {
            Nom = nom;
            Reference = reference;
            Prix = prix;
            Quantite = quantite;
            Categorie = categorie;
        }

        public Produit(string nom,  double prix, int quantite, string categorie)
        {
            Nom = nom;
            Prix = prix;
            Quantite = quantite;
            Categorie = categorie;
        }

        public override string ToString()
        {
            return $"Référence du produit : {Reference}\n" +
                   $"Nom du produit : {Nom}\n" +
                   $"Prix du produit : {Prix}\n" +
                   $"Quantité du produit : {Quantite}\n" +
                   $"Catégorie du produit : {Categorie}" +
                   "\n";
        }
        
        public double ValeurStock()
        {
            return Prix * Quantite;
        }
    }
}