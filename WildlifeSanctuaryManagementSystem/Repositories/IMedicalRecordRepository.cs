using System.Threading.Tasks;
using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public interface IMedicalRecordRepository
    {
        Task<IEnumerable<MedicalRecord>> GetAllMedicalRecords();
        Task<IEnumerable<MedicalRecord>> GetByVetId(int vetId);
        Task<MedicalRecord> GetMedicalRecordById(int id);
        Task AddMedicalRecord(MedicalRecord medicalRecord);
        Task UpdateMedicalRecord(MedicalRecord medicalRecord);
        Task DeleteMedicalRecord(int id);

        Task<IEnumerable<dynamic>> GetMedicalRecordsByVet(int vetId);
    }
}
