using TasteTest.Models;

namespace TasteTest.Services
{
    public class DettaglioOrdineViewModel
    {
        public ClientiViewModel Cliente { get; set; } = new();
        public int IDOrdine { get; set; }
        public List<DettaglioProdottoViewModel> Prodotti { get; set; } = new();
        public decimal Totale => Prodotti.Sum(p => p.Quantita * p.PrezzoUnitario);
    }

    public class DettaglioProdottoViewModel
    {
        public string NomeProdotto { get; set; } = string.Empty;
        public int Quantita { get; set; }
        public decimal PrezzoUnitario { get; set; }
    }

}