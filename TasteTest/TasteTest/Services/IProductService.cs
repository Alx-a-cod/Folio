using TasteTest.Models;

namespace TasteTest.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Prodotti>> GetAllProdAsync();
        Task<Prodotti?> GetByIdProdAsync(int idProdotto);
        Task<bool> AddProdAsync(Prodotti prodotto);
        Task<bool> UpdateProdAsync(Prodotti prodotto);
        Task<bool> DeleteProdAsync(int idProdotto);
        Task<decimal?> GetPrezzoProdottoAsync(int idProdotto);
    }
}