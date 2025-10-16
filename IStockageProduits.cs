namespace ProducInventory
{
    interface IStockageProduits
    {
        List<Produit> Charger();
        void Sauvegarder( List<Produit>? produits = null);
    }
}