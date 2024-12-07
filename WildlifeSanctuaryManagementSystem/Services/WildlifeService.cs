using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Repositories;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public class WildlifeService:IWildlifeService
    {
        


            private readonly IWildlifeRepository _repository;

        public WildlifeService(IWildlifeRepository repository)
        {
            _repository = repository;
        }

        // Get all wildlife data
        public async Task<IEnumerable<WildlifeData>> GetAllWildlifeData()
        {
            return await _repository.GetAllWildlifeData();
        }

        public async Task<IEnumerable<WildlifeData>> GetWildlifeDataByBiologist(int biologistId)
        {
            return await _repository.GetWildlifeDataByBiologistId(biologistId);
        }

        // Get wildlife data by ID
        public async Task<WildlifeData> GetWildlifeDataById(int dataId)
        {
            return await _repository.GetWildlifeDataById(dataId);
        }

        // Get wildlife data by sanctuary ID
        public async Task<List<WildlifeData>> GetWildlifeDataBySanctuary(int sanctuaryId)
        {
            return await _repository.GetWildlifeDataBySanctuary(sanctuaryId);
        }

        public async Task<IEnumerable<object>> GetTopRecentObservationsAsync(int id)
        {
            return await _repository.GetTopRecentObservationsAsync(id);
        }
        public async Task<List<WildlifeData>> GetPopulationTrendsAsync()
        {
            return await _repository.GetPopulationTrendsAsync();
        }

        // Add new wildlife data
        public async Task AddWildlifeData(WildlifeData wildlifeData)
        {
            await _repository.AddWildlifeData(wildlifeData);
        }

        // Update existing wildlife data
        public async Task UpdateWildlifeData(WildlifeData wildlifeData)
        {
            await _repository.UpdateWildlifeData(wildlifeData);
        }

        // Delete wildlife data
        public async Task DeleteWildlifeData(int dataId)
        {
            await _repository.DeleteWildlifeData(dataId);
        }
    }
    
}
