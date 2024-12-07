using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Repositories;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public class EnvironmentalService : IEnvironmentalService
    {
        private readonly IEnvironmentalRepository _repository;

        public EnvironmentalService(IEnvironmentalRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EnvironmentalData>> GetAllData()
        {
            return await _repository.GetAllData();
        }

        public async Task<IEnumerable<EnvironmentalData>> GetEnvironmentalDataByBiologistId(int biologistId)
        {
            return await _repository.GetEnvironmentalDataByBiologistId(biologistId);
        }


        public async Task<EnvironmentalData> GetDataById(int id)
        {
            return await _repository.GetDataById(id);
        }

        public async Task<IEnumerable<EnvironmentalData>> GetByConductedBy(int conductedById)
        {
            return await _repository.GetByConductedBy(conductedById);
        }

        public async Task AddData(EnvironmentalData environmentalData)
        {
            await _repository.AddData(environmentalData);
        }

        public async Task UpdateData(EnvironmentalData environmentalData)
        {
            await _repository.UpdateData(environmentalData);
        }

        public async Task DeleteData(int id)
        {
            await _repository.DeleteData(id);
        }

        public async Task<IEnumerable<Object>> GetAssessmentsBySanctuary()
        {
            return await _repository.GetAssessmentsBySanctuary();
        }

        public async Task<IEnumerable<Object>> GetAssessmentsByImpactType()
        {
            return await _repository.GetAssessmentsByImpactType();
        }
    }
}
