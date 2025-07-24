using System.Data;
using Dapper;
using TasteTest.Models;
using TasteTest.Utility;
using TasteTest.ViewModels;

namespace TasteTest.Services
{
    public class OrderService : IOrderService
    {
        private readonly IDbLayer _dbLayer;
        private readonly ILogger<OrderService> _logger;
        private readonly IDettService _dettService;
        private readonly IProductService _productService;

        public OrderService(IDbLayer dbLayer, ILogger<OrderService> logger, IDettService dettService, IProductService productService)
        {
            _dbLayer = dbLayer;
            _logger = logger;
            _dettService = dettService;
            _productService = productService;
        }

        public async Task<IEnumerable<Ordine>> GetAllOrdAsync()
        {
            const string sql = "SELECT * FROM Ordini";
            return await _dbLayer.QueryAsync<Ordine>(sql);
        }

        public async Task<Ordine> GetByIdOrdAsync(int idOrdine)
        {
            const string sql = "SELECT * FROM Ordini WHERE IDOrdine = @idOrdine";
            var result = await _dbLayer.QueryAsync<Ordine>(sql, new { idOrdine });
            return result.FirstOrDefault() ?? new Ordine();
        }

        public async Task<bool> AddOrdAsync(Ordine ordine)
        {
            const string sql = "INSERT INTO Ordini (IDCliente, Totale, DataOrdini) VALUES (@IDCliente, @Totale, @DataOrdini)";
            var rows = await _dbLayer.ExecuteAsync(sql, new
            {
                ordine.IDCliente,
                ordine.Totale,
                DataOrdini = ordine.DataOrdini == default ? DateTime.Now : ordine.DataOrdini,
               
            });
            return rows > 0;
        }

        public async Task<bool> UpdateOrdAsync(Ordine ordine)
        {
            const string sql = @"UPDATE Ordini SET IDCliente = @IDCliente, Totale = @Totale, DataOrdini = @DataOrdini WHERE IDOrdine = @IDOrdine";
            var rows = await _dbLayer.ExecuteAsync(sql, ordine);
            return rows > 0;
        }

        public async Task<bool> DeleteOrdineAsync(int ordineId)
        {
            const string sql = "DELETE FROM Ordini WHERE IDOrdine = @ordineId";
            var rows = await _dbLayer.ExecuteAsync(sql, new { ordineId });
            return rows > 0;
        }

        // <°)))>< -- potrebbe essere carino da implementare in futuro. (idea: una logica di statistiche da legare a powerBI)
        //commentato per eventuali sviluppi futuri.

        //public async Task<Ordine?> GetOrdineAttivoPerCliente(int clienteId)
        //{
        //    const string sql = "SELECT TOP 1 * FROM Ordini WHERE IDCliente = @clienteId AND Stato = 'In Corso' ORDER BY DataOrdini DESC";
        //    var result = await _dbLayer.QueryAsync<Ordine>(sql, new { clienteId });
        //    return result.FirstOrDefault();
        //}

        //sarebbe carina anche una tabella stato. 'In Corso', 'Completato', 'Annullato' etc.

        public async Task<bool> AggiungiProdottoAlCarrello(int clienteId, int prodottoId, int quantita)
        {
            using var conn = _dbLayer.GetConnection();
            using var transaction = conn.BeginTransaction();

            try
            {
                // Controlla se esiste un carrello aperto per il cliente
                const string sqlOrdine = "SELECT TOP 1 IDOrdine FROM Ordini WHERE IDCliente = @clienteId";
                var ordineId = await conn.ExecuteScalarAsync<int?>(sqlOrdine, new { clienteId }, transaction);

                if (ordineId == null)
                {
                    const string insertOrdine = "INSERT INTO Ordini (IDCliente, DataOrdini) OUTPUT INSERTED.IDOrdine VALUES (@clienteId, GETDATE())";
                    ordineId = await conn.ExecuteScalarAsync<int>(insertOrdine, new { clienteId }, transaction);
                }

                // Prende prezzo prodotto
                const string sqlPrezzo = "SELECT PrezzoUnitario FROM Prodotti WHERE IDProdotto = @prodottoId";
                var prezzoUnitario = await conn.ExecuteScalarAsync<decimal?>(sqlPrezzo, new { prodottoId }, transaction);
                if (prezzoUnitario == null)
                {
                    transaction.Rollback();
                    return false;
                }

                // Inserisci o aggiorna dettaglio ordine
                // Verifica se prodotto è già nel carrello
                const string sqlCheckDettaglio = "SELECT COUNT(1) FROM DettaglioOrdini WHERE IDOrdine = @ordineId AND IDProdotto = @prodottoId";
                var count = await conn.ExecuteScalarAsync<int>(sqlCheckDettaglio, new { ordineId, prodottoId }, transaction);

                if (count > 0)
                {
                    // Aggiorna quantità e totale
                    const string sqlUpdateDettaglio = @"UPDATE DettaglioOrdini SET Quantita = Quantita + @quantita, Totale = Totale + @totale WHERE IDOrdine = @ordineId AND IDProdotto = @prodottoId";
                    var totale = prezzoUnitario.Value * quantita;
                    await conn.ExecuteAsync(sqlUpdateDettaglio, new { ordineId, prodottoId, quantita, totale }, transaction);
                }
                else
                {
                    // Inserisci nuovo dettaglio
                    const string sqlInsertDettaglio = @"INSERT INTO DettaglioOrdini (IDOrdine, IDProdotto, Quantita, Totale) VALUES (@ordineId, @prodottoId, @quantita, @totale)";
                    var totale = prezzoUnitario.Value * quantita;
                    await conn.ExecuteAsync(sqlInsertDettaglio, new { ordineId, prodottoId, quantita, totale }, transaction);
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore in AggiungiProdottoAlCarrello");
                return false;
            }
        }

        public async Task<bool> RimuoviProdottoDalCarrello(int ordineId, int prodottoId)
        {
            const string sql = "DELETE FROM DettaglioOrdini WHERE IDOrdine = @ordineId AND IDProdotto = @prodottoId";
            var rows = await _dbLayer.ExecuteAsync(sql, new { ordineId, prodottoId });
            return rows > 0;
        }

        public async Task<bool> AnnullaOrdine(int ordineId)
        {
            using var conn = _dbLayer.GetConnection();
            using var transaction = conn.BeginTransaction();
            try
            {
                const string sqlDettagli = "DELETE FROM DettaglioOrdini WHERE IDOrdine = @ordineId";
                await conn.ExecuteAsync(sqlDettagli, new { ordineId }, transaction);

                const string sqlOrdine = "DELETE FROM Ordini WHERE IDOrdine = @ordineId";
                var rows = await conn.ExecuteAsync(sqlOrdine, new { ordineId }, transaction);

                transaction.Commit();
                return rows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore in AnnullaOrdine");
                return false;
            }
        }

        public async Task<bool> CompletaOrdine(int clienteId, List<ProdottoOrdineDto> prodotti)
        {
            if (prodotti == null || prodotti.Count == 0)
                return false;

            using var conn = _dbLayer.GetConnection();
            using var transaction = conn.BeginTransaction();

            try
            {
                decimal totaleOrdine = 0;
                foreach (var p in prodotti)
                {
                    var prezzo = await _productService.GetPrezzoProdottoAsync(p.ProdottoId) ?? 0;
                    totaleOrdine += prezzo * p.Quantita;
                }

                const string insertOrdine = @"INSERT INTO Ordini (IDCliente, Totale, DataOrdini) VALUES (@clienteId, @totale, @dataOrdini)"; 
                await conn.ExecuteAsync(insertOrdine, new { clienteId, totale = totaleOrdine, dataOrdini = DateTime.Now }, transaction);

                const string selectId = "SELECT TOP 1 IDOrdine FROM Ordini ORDER BY IDOrdine DESC";
                var idOrdine = await conn.ExecuteScalarAsync<int>(selectId, transaction: transaction);

                const string insertDettaglio = @"INSERT INTO DettaglioOrdini (IDOrdine, IDProdotto, Quantita) VALUES (@idOrdine, @idProdotto, @quantita)";
                foreach (var p in prodotti)
                {
                    await conn.ExecuteAsync(insertDettaglio, new { idOrdine, idProdotto = p.ProdottoId, quantita = p.Quantita }, transaction);
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore in CompletaOrdine");
                transaction.Rollback();
                return false;
            }
        }

        public async Task<List<OrdineViewModel>> GetOrdiniAsync()
        {
            const string sql = @"
                SELECT o.IDOrdine, CONCAT(c.Nome, ' ', c.Cognome) AS Cliente, o.Totale, o.DataOrdini 
                FROM Ordini o 
                JOIN Clienti c ON o.IDCliente = c.IDCliente";

            var ordini = await _dbLayer.QueryAsync<OrdineViewModel>(sql);
            return [.. ordini];
        }

        
        public async Task<List<DettaglioOrdineViewModel>> GetDettagliOrdineAsync(int ordineId)
        {
          // Step 1: Prende dati ordine + cliente
          const string sqlOrdineCliente = @"
                                            SELECT 
                                                o.IDOrdine,
                                                c.Nome,
                                                c.Cognome,
                                                c.Email
                                            FROM Ordini o
                                            JOIN Clienti c ON o.IDCliente = c.IDCliente
                                            WHERE o.IDOrdine = @ordineId";

          var ordineCliente = await _dbLayer.QueryAsync<(int IDOrdine, string Nome, string Cognome, string Email)>(sqlOrdineCliente, new { ordineId });

          var ordineClienteSingolo = ordineCliente.FirstOrDefault();

          if (ordineClienteSingolo == default)
          return null; // Ordine non trovato

            // Step 2: Prende i prodotti nel dettaglio ordine
            const string sqlProdotti = @"
                                         SELECT 
                                            p.Descrizione AS NomeProdotto,
                                            d.Quantita,
                                            p.PrezzoUnitario
                                         FROM DettaglioOrdini d
                                         JOIN Prodotti p ON d.IDProdotto = p.IDProdotto
                                         WHERE d.IDOrdine = @ordineId";

            var prodotti = await _dbLayer.QueryAsync<DettaglioProdottoViewModel>(sqlProdotti, new { ordineId });

            // Step 3: Componi il modello completo
            var dettaglioOrdine = new DettaglioOrdineViewModel
            {
                IDOrdine = ordineClienteSingolo.IDOrdine,
                Cliente = new ClientiViewModel
                {
                    Nome = ordineClienteSingolo.Nome,
                    Cognome = ordineClienteSingolo.Cognome,
                    Email = ordineClienteSingolo.Email
                },
                Prodotti = [.. prodotti]
            };

            return [dettaglioOrdine];
        }

        public async Task<DettaglioOrdineViewModel> GetDettaglioOrdineAsync(int idOrdine)
        {
            var dettagli = await _dettService.GetAllDetAsync();
            var dettagliOrdine = dettagli.Where(d => d.IDOrdine == idOrdine);

            var prodottiDettaglio = new List<DettaglioProdottoViewModel>();

            foreach (var dettaglio in dettagliOrdine)
            {
                var prodotto = await _productService.GetByIdProdAsync(dettaglio.IDProdotto);

                prodottiDettaglio.Add(new DettaglioProdottoViewModel
                {
                    NomeProdotto = prodotto?.Descrizione ?? "Prodotto sconosciuto",
                    Quantita = dettaglio.Quantità,
                    PrezzoUnitario = prodotto?.PrezzoUnitario ?? 0m,
                    
                });
            }

            return new DettaglioOrdineViewModel
            {
                IDOrdine = idOrdine,
                Prodotti = prodottiDettaglio
            };
        }
    }
}



