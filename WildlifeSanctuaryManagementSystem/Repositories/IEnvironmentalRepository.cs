using Microsoft.EntityFrameworkCore;
using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public interface IEnvironmentalRepository
    {
        Task<IEnumerable<EnvironmentalData>> GetAllData();
        Task<IEnumerable<EnvironmentalData>> GetEnvironmentalDataByBiologistId(int biologistId);
        Task<EnvironmentalData> GetDataById(int id);
        Task<IEnumerable<EnvironmentalData>> GetByConductedBy(int conductedById);

        Task<IEnumerable<Object>> GetAssessmentsBySanctuary();



        Task<IEnumerable<Object>> GetAssessmentsByImpactType();
        
            Task AddData(EnvironmentalData environmentalData);
        Task UpdateData(EnvironmentalData environmentalData);
        Task DeleteData(int id);
    }
}
