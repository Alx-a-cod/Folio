using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;


// legenda markers: 

// <°)))>< = generic markdown
// ~:>-__- = soluzione ''strisciante''. workaroud scivolosi. Hacky fix. Dangerous logic.
// <3)~~   = tracce, test, breakpoints. Debug + explore
// くコ:彡  = troppi parametri, troppi metodi, troppi livelli di astrazione
// ᓚᘏᗢ    = kitty-code, Clean. ≽^•⩊•^≼˚ 
// (\_/)   = bunny code, non-linear logic leaps.    
// 🦆 // dummy. placeholder 
// 🐧 =  waddle code. Messy / convoluted. Hard to navigate, understand or modify due to complexity / structure
// 🐢 = turtle code. Slow, inefficient, or poorly performing code.
// 💀 = nope. 
// 🦉 = night-coding. 


namespace TasteTest.Utility
{
    public interface IDbLayer
    {
        Task<int> ExecuteAsync(string sql, object? parameters = null);
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null);
        Task<object?> ExecuteScalarAsync(string sql, object? parameters = null);
        Task<T?> ExecuteScalarAsync<T>(string sql, object? parameters = null);
        SqlConnection GetConnection();
    }

    public class DbLayer : IDbLayer
    {
        private readonly string _connectionString;

        public DbLayer(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection GetConnection()
        {
            var conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
        }

        public async Task<int> ExecuteAsync(string sql, object? parameters = null)
        {
            using var conn = GetConnection();
            return await conn.ExecuteAsync(sql, parameters);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
        {
            using var conn = GetConnection();
            return await conn.QueryAsync<T>(sql, parameters);
        }
        public async Task<T?> ExecuteScalarAsync<T>(string sql, object? parameters = null)
        {
            using var conn = GetConnection();
            return await conn.ExecuteScalarAsync<T>(sql, parameters);
        }

        public async Task<object?> ExecuteScalarAsync(string sql, object? parameters = null)
        {
            using var conn = GetConnection();
            return await conn.ExecuteScalarAsync(sql, parameters);
        }
    }
}