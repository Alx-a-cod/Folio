namespace TasteTest.Models
{
    public class Dettagli_Ord
    {

        public int IDDettaglio { get; set; } // primary key
        public int IDOrdine { get; set; } // id cliente 
        public int IDProdotto { get; set; }
        public int Quantità { get; set; } //data ordine cliente 

        //costruttore vuoto, optional 
        public Dettagli_Ord() { }

        //costruttore parametrico
        public Dettagli_Ord(int iddettaglio, int idordine, int idprodotto, int quantità)
        {
            IDDettaglio = iddettaglio;
            IDOrdine = idordine;
            IDProdotto = idprodotto;
            Quantità = quantità;
        }



    }
    public class OrdineViewModel
    {
        public int IDOrdine { get; set; }
        public string Cliente { get; set; }
        public string Descrizione { get; set; }
        public decimal Totale { get; set; }
    }

    public class DettaglioOrdineViewModel
    {
        public int IDProdotto { get; set; }
        public string Descrizione { get; set; }
        public int Quantita { get; set; }
    }


}
