using TasteTest.Models;

namespace TasteTest.ViewModels
{
    public class AcquistaViewModel
    {
        public List<Prodotti> Prodotti { get; set; } = new List<Prodotti>();  // Per i prodotti disponibili
        public Ordine? Ordine { get; set; }  // Ordine attuale del cliente
    }

    public class CompletaOrdineRequest
    {
        public int ClienteId { get; set; }
        public List<ProdottoOrdineDto> Prodotti { get; set; }
    }

    public class ProdottoOrdineDto
    {
        public int ProdottoId { get; set; }
        public int Quantita { get; set; }
        public decimal Prezzo { get; set; }
        public decimal Totale { get; set; }
    }

}
