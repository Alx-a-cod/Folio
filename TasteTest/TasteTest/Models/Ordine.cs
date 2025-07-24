namespace TasteTest.Models
{
    public class Ordine
    {
        public int IDOrdine { get; set; } // primary key
        public int IDCliente { get; set; } // descrizione prodotto
        public decimal Totale { get; set; } //prezzo tutti i pezzi
        public DateTime DataOrdini { get; set; }
        //costruttore vuoto optional
        public Ordine() { }

        // costruttore parametrico

        public Ordine(int Idordine, int IdCliente, decimal totale, DateTime dataordini)
        {
            IDOrdine = Idordine;
            IDCliente = IdCliente;
            Totale = totale;
            DataOrdini = dataordini;
        }

        //Proprietà non mappata: non viene salvata nel DB, serve solo per il codice
        public List<Dettagli_Ord>? Dettagli { get; set; }= [];  // Lista Dettagli

        // AGGIUNGIAMO QUESTA PROPRIETÀ PER RISOLVERE L'ERRORE
        public string Stato { get; set; } = "In Corso";  // Default a "In Corso"

    }
}

  
