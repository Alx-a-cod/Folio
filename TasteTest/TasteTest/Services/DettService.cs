using System.Data;
using TasteTest.Models;
using TasteTest.Utility;

namespace TasteTest.Services
{
    public class DettService : IDettService
    {
        private readonly IDbLayer _dbLayer;

        public DettService(IDbLayer dbLayer)
        {
            _dbLayer = dbLayer;
        }

        public async Task<IEnumerable<Dettagli_Ord>> GetAllDetAsync()
        {
            const string sql = "SELECT * FROM Dettagli_Ord";
            return await _dbLayer.QueryAsync<Dettagli_Ord>(sql);
        }

        public async Task<Dettagli_Ord?> GetByIdDetAsync(int idDettaglio)
        {
            const string sql = "SELECT * FROM Dettagli_Ord WHERE IDDettaglio = @IdDettaglio";
            var risultati = await _dbLayer.QueryAsync<Dettagli_Ord>(sql, new { IdDettaglio = idDettaglio });
            return risultati.FirstOrDefault();
        }

        public async Task<bool> AddDetAsync(Dettagli_Ord dettaglio)
        {
            const string sql = @"
                INSERT INTO Dettagli_Ord (IDOrdine, IDProdotto, Quantità)
                VALUES (@IDOrdine, @IDProdotto, @Quantità)";

            int righe = await _dbLayer.ExecuteAsync(sql, new
            {
                dettaglio.IDOrdine,
                dettaglio.IDProdotto,
                dettaglio.Quantità
            });
            return righe > 0;
        }

        public async Task<bool> UpdateDetAsync(Dettagli_Ord dettaglio)
        {
            const string sql = @"
                UPDATE Dettagli_Ord
                SET IDOrdine = @IDOrdine,
                    IDProdotto = @IDProdotto,
                    Quantità = @Quantità
                WHERE IDDettaglio = @IDDettaglio";

            int righe = await _dbLayer.ExecuteAsync(sql, new
            {
                dettaglio.IDOrdine,
                dettaglio.IDProdotto,
                dettaglio.Quantità,
                dettaglio.IDDettaglio
            });
            return righe > 0;
        }

        public async Task<bool> DeleteDetAsync(Dettagli_Ord dettaglio)
        {
            const string sql = "DELETE FROM Dettagli_Ord WHERE IDDettaglio = @IDDettaglio";
            int righe = await _dbLayer.ExecuteAsync(sql, new { dettaglio.IDDettaglio });
            return righe > 0;
        }

    }
}
