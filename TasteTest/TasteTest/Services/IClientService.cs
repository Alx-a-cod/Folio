using TasteTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TasteTest.Services
{
    public interface IClientService
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente?> GetByIdAsync(int id);
        Task<int> AddAsync(Cliente cliente);
        Task<bool> UpdateAsync(Cliente cliente);
        Task<bool> DeleteAsync(int id);
    }
}
