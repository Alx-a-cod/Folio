using TasteTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using TasteTest.ViewModels;

namespace TasteTest.Services
{
    public interface IOrderService
    {
        public Task<IEnumerable<Ordine>> GetAllOrdAsync();
        public Task<Ordine> GetByIdOrdAsync(int Idordine);
        public Task<bool> AddOrdAsync(Ordine ordine);
        public Task<bool> UpdateOrdAsync(Ordine ordine);
        Task<DettaglioOrdineViewModel> GetDettaglioOrdineAsync(int idOrdine);

        //per la gestione della pagina acquista

        //<°)))><
        //Task<Ordine?> GetOrdineAttivoPerCliente(int clienteId);

        public Task<bool> AggiungiProdottoAlCarrello(int clienteId, int prodottoId, int quantita);

        public Task<bool> RimuoviProdottoDalCarrello(int ordineId, int prodottoId);
        public Task<bool> AnnullaOrdine(int ordineId);
        public Task<bool> CompletaOrdine(int clienteId, List<ProdottoOrdineDto> prodotti);

        
        public Task<List<OrdineViewModel>> GetOrdiniAsync();
        public Task<bool> DeleteOrdineAsync(int ordineId);
        public Task<List<DettaglioOrdineViewModel>> GetDettagliOrdineAsync(int ordineId);
       

    }
}