namespace ProducInventory
{
    interface IStockageProduits
    {
        List<Produit> Charger(string cheminFichier);
        void Sauvegarder(string cheminFichier, List<Produit>? produits = null);
    }
}