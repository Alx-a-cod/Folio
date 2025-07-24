using TasteTest.Models;

namespace TasteTest.Services
{
    public interface IDettService
    {
        Task<IEnumerable<Dettagli_Ord>> GetAllDetAsync();
        Task<Dettagli_Ord?> GetByIdDetAsync(int iddettaglio);
        Task<bool> AddDetAsync(Dettagli_Ord dettaglio);
        Task<bool> UpdateDetAsync(Dettagli_Ord dettaglio);
        Task<bool> DeleteDetAsync(Dettagli_Ord dettaglio);
    }
}
//namespace TasteTest.Services
//{
//    public interface IDettService
//    {
//        public Task<IEnumerable<Dettagli_Ord>> GetAllDetAsync();
//        public Task<Dettagli_Ord> GetByIdDetAsync(int iddettaglio);
//        public Task<bool> AddDetAsync(Dettagli_Ord dettaglio);
//        public Task<bool> UpdateDetAsync(Dettagli_Ord dettaglio);
//        public Task<bool> DeleteDetAsync(Dettagli_Ord dettaglio);
//    }
//}

