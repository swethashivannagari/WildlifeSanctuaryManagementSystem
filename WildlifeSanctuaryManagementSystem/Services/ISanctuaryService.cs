using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public interface ISanctuaryService
    {
        public Task<IEnumerable<Sanctuary>> GetAllSanctuaries();
       public Task<Sanctuary> GetSanctuaryById(int id);
       public Task AddSanctuary(Sanctuary sanctuary);
       public Task UpdateSanctuary(Sanctuary sanctuary);
       public Task DeleteSanctuaryById(int id);
        public Task<bool> CheckSanctuaryExists(string name, string location);
        Task<IEnumerable<object>> GetSanctuariesWithIds();
       
    }
}
