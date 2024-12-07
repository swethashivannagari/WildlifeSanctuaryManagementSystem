using System.Threading.Tasks;
using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public interface IWildlifeService
    {
        Task<IEnumerable<WildlifeData>> GetAllWildlifeData();
        Task<WildlifeData> GetWildlifeDataById(int dataId);

        Task<IEnumerable<WildlifeData>> GetWildlifeDataByBiologist(int biologistId);
        Task<List<WildlifeData>> GetWildlifeDataBySanctuary(int sanctuaryId);
        Task<IEnumerable<object>> GetTopRecentObservationsAsync(int id);
        Task<List<WildlifeData>> GetPopulationTrendsAsync();

        Task AddWildlifeData(WildlifeData wildlifeData);
        Task UpdateWildlifeData(WildlifeData wildlifeData);
        Task DeleteWildlifeData(int dataId);
    }
}
