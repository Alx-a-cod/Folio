using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TasteTest.Models;
using TasteTest.Utility;

namespace TasteTest.Services
{
    public class ProductService : IProductService
    {
        private readonly IDbLayer _dbLayer;

        public ProductService(IDbLayer dbLayer)
        {
            _dbLayer = dbLayer;
        }

        public async Task<IEnumerable<Prodotti>> GetAllProdAsync()
        {
            string query = "SELECT * FROM Prodotti";
            using var conn = _dbLayer.GetConnection();
            return await conn.QueryAsync<Prodotti>(query);
        }

        public async Task<Prodotti?> GetByIdProdAsync(int idProdotto)
        {
            string query = "SELECT * FROM Prodotti WHERE IDProdotto = @IDProdotto";
            using var conn = _dbLayer.GetConnection();
            return await conn.QueryFirstOrDefaultAsync<Prodotti>(query, new { IDProdotto = idProdotto });
        }

        public async Task<bool> AddProdAsync(Prodotti prodotto)
        {
            string query = @"
                INSERT INTO Prodotti (Descrizione, PrezzoUnitario)
                VALUES (@Descrizione, @PrezzoUnitario)";
            using var conn = _dbLayer.GetConnection();
            var rows = await conn.ExecuteAsync(query, prodotto);
            return rows > 0;
        }

        public async Task<bool> UpdateProdAsync(Prodotti prodotto)
        {
            string query = @"
                UPDATE Prodotti 
                SET Descrizione = @Descrizione, PrezzoUnitario = @PrezzoUnitario
                WHERE IDProdotto = @IDProdotto";
            using var conn = _dbLayer.GetConnection();
            var rows = await conn.ExecuteAsync(query, prodotto);
            return rows > 0;
        }

        public async Task<bool> DeleteProdAsync(int idProdotto)
        {
            string query = "DELETE FROM Prodotti WHERE IDProdotto = @IDProdotto";
            using var conn = _dbLayer.GetConnection();
            var rows = await conn.ExecuteAsync(query, new { IDProdotto = idProdotto });
            return rows > 0;
        }

        public async Task<decimal?> GetPrezzoProdottoAsync(int idProdotto)
        {
            string query = "SELECT PrezzoUnitario FROM Prodotti WHERE IDProdotto = @IDProdotto";
            using var conn = _dbLayer.GetConnection();
            return await conn.ExecuteScalarAsync<decimal?>(query, new { IDProdotto = idProdotto });
        }
    }
}