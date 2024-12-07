using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public interface IWildlifeRepository
    {

        Task<IEnumerable<WildlifeData>> GetAllWildlifeData();
        Task<WildlifeData> GetWildlifeDataById(int dataId);
        Task AddWildlifeData(WildlifeData wildlifeData);
        Task UpdateWildlifeData(WildlifeData wildlifeData);
        Task DeleteWildlifeData(int dataId);
        Task<List<WildlifeData>> GetWildlifeDataBySanctuary(int sanctuaryId);
        Task<List<WildlifeData>> GetPopulationTrendsAsync();
        Task<IEnumerable<object>> GetTopRecentObservationsAsync(int id);
        Task<IEnumerable<WildlifeData>> GetWildlifeDataByBiologistId(int biologistId);

    }
}
