using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public interface IMedicalRecordService
    {
        Task<IEnumerable<MedicalRecord>> GetAllMedicalRecords();
        Task<IEnumerable<MedicalRecord>> GetByVetId(int vetId);
        Task<MedicalRecord> GetMedicalRecordById(int id);
        Task AddMedicalRecord(MedicalRecord medicalRecord);
        Task UpdateMedicalRecord(MedicalRecord medicalRecord);
        Task DeleteMedicalRecord(int id);

        Task<IEnumerable<dynamic>> GetMedicalRecordsByVet(int vetId);

        Task<IEnumerable<dynamic>> GetTop5MedicalRecordsFromToday(int vetId);
        Task<int> GetMedicalRecordsCount(int vetId);
    }
}
