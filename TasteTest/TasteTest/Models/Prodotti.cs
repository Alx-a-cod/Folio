namespace TasteTest.Models
{
    public class Prodotti
    {
        public int? IDProdotto { get; set; } // primary key
        public string Descrizione { get; set; } // descrizione prodotto unicode
        public decimal PrezzoUnitario { get; set; } //costo prodotto singolo  

        //costruttore vuoto, optional 
        public Prodotti() { }

        //costruttore parametrico
        public Prodotti(int Idprodotto, string descrizione, decimal prezzounitario)
        {
            IDProdotto = Idprodotto;
            Descrizione = descrizione;
            PrezzoUnitario = prezzounitario;
        
        }
    }
}