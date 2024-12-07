using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public interface ISanctuaryRepository
    {
       public Task<IEnumerable<Sanctuary>> GetAllSanctuaries();
       public Task<Sanctuary> GetSanctuaryById(int id);
       public Task AddSanctuary(Sanctuary santuary);
       public Task UpdateSanctuary(Sanctuary sanctuary);
       public Task DeleteSanctuaryById(int id);
        public  Task<bool> SanctuaryExists(string name, string location);


    }
}
