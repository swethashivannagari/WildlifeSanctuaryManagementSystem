using Microsoft.EntityFrameworkCore;
using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Repositories;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public class SanctuaryService:ISanctuaryService
    {
        private readonly ISanctuaryRepository _repository;

        public SanctuaryService(ISanctuaryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Sanctuary>> GetAllSanctuaries()
        {
            return await _repository.GetAllSanctuaries();
        }

        public async Task<Sanctuary> GetSanctuaryById(int id)
        {
            return await _repository.GetSanctuaryById(id);
        }

        public async Task AddSanctuary(Sanctuary sanctuary)
        {
           
           
            await _repository.AddSanctuary(sanctuary);
        }

        public async Task UpdateSanctuary(Sanctuary sanctuary)
        {

            await _repository.UpdateSanctuary(sanctuary);
        }

        public async Task DeleteSanctuaryById(int id)
        {
            await _repository.DeleteSanctuaryById(id);
        }

        public async Task<bool> CheckSanctuaryExists(string name, string location)
        {
            return await _repository.SanctuaryExists(name, location);
        }

        public async Task<IEnumerable<object>> GetSanctuariesWithIds()
        {
            var sanctuaries = await _repository.GetAllSanctuaries();
               return sanctuaries
            .Select(s => new { s.SanctuaryId, s.Name })
            .ToList();
        }

        public async Task<User> GetManagerById(int managerId)
        {
            var sanctuaries = await _repository.GetAllSanctuaries();
            var sanctuary = sanctuaries.FirstOrDefault(s => s.ManagerId == managerId);
            return sanctuary?.Manager;
        }

    }

    
}
