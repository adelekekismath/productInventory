namespace ProducInventory
{
    class Produit {
        private string Nom { get; set; }
        private string? Reference { get; set; }
        private double Prix { get; set; }
        private int Quantite { get; set; }
        private string Categorie { get; set; }

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

        public override bool Equals(object? obj)
        {
            if (obj is Produit other)
            {
                return Reference == other.Reference &&
                       Nom == other.Nom &&
                       Prix == other.Prix &&
                       Quantite == other.Quantite &&
                       Categorie == other.Categorie;
            }
            return false;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(Reference, Nom, Prix, Quantite, Categorie);
        }
    }
}