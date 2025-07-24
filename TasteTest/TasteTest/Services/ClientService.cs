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
    public class ClientService : IClientService
    {
        private readonly IDbLayer _dbLayer;

        public ClientService(IDbLayer dbLayer)
        {
            _dbLayer = dbLayer;
        }


        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            var query = "SELECT * FROM Clienti";
            using var conn = _dbLayer.GetConnection();
            return await conn.QueryAsync<Cliente>(query);
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Clienti WHERE IDCliente = @ID";
            using var conn = _dbLayer.GetConnection();
            return await conn.QueryFirstOrDefaultAsync<Cliente>(query, new { ID = id });
        }

        public async Task<int> AddAsync(Cliente cliente)
        {
            var query = @"
        INSERT INTO Clienti (Nome, Cognome, Email)
        VALUES (@Nome, @Cognome, @Email);
        SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using var conn = _dbLayer.GetConnection();
            var id = await conn.ExecuteScalarAsync<int>(query, cliente); // <- questo è il fix!
            return id;
        }

        public async Task<bool> UpdateAsync(Cliente cliente)
        {
            var query = @"UPDATE Clienti 
                      SET Nome = @Nome, Cognome = @Cognome, Email = @Email 
                      WHERE IDCliente = @IDCliente";
            using var conn = _dbLayer.GetConnection();
            var rows = await conn.ExecuteAsync(query, cliente);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var query = "DELETE FROM Clienti WHERE IDCliente = @ID";
            using var conn = _dbLayer.GetConnection();
            var rows = await conn.ExecuteAsync(query, new { ID = id });
            return rows > 0;
        }
    }
}





