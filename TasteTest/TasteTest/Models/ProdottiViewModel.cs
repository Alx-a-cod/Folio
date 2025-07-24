namespace TasteTest.Models
{
    public class ProdottiViewModel
    {

        public Prodotti Prodotto { get; set; } = new Prodotti();
        public IEnumerable<Prodotti> ListaProdotti { get; set; } = new List<Prodotti>();


    }
}
