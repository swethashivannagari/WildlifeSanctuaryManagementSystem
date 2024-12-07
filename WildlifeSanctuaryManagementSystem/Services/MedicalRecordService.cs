using Microsoft.EntityFrameworkCore;
using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Repositories;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public class MedicalRecordService:IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _repository;
        private readonly INotificationService _notificationService;

        public MedicalRecordService(IMedicalRecordRepository repository,INotificationService notificationService)
        {
            _repository = repository;
            _notificationService = notificationService;
        }

        public async Task<IEnumerable<MedicalRecord>> GetAllMedicalRecords()
        {
            return await _repository.GetAllMedicalRecords();
        }
        public async Task<IEnumerable<MedicalRecord>> GetByVetId(int vetId)
        {
            return await _repository.GetByVetId(vetId);
        }
        public async Task<MedicalRecord> GetMedicalRecordById(int id)
        {
            try
            {
                return await _repository.GetMedicalRecordById(id);
            }
            catch (Exception ex)
            {
                throw new Exception( ex.Message);
            }
        }

        public async Task AddMedicalRecord(MedicalRecord medicalRecord)
        {
            try
            {
                await _repository.AddMedicalRecord(medicalRecord);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
        }

        public async Task UpdateMedicalRecord(MedicalRecord medicalRecord)
        {
            try
            {
                await _repository.UpdateMedicalRecord(medicalRecord);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
        }
        public async Task DeleteMedicalRecord(int id)
        {
            try
            {
                await _repository.DeleteMedicalRecord(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
        }

        public async Task<IEnumerable<dynamic>> GetMedicalRecordsByVet(int vetId)
        {
            return await _repository.GetMedicalRecordsByVet(vetId);
        }

        public async Task<IEnumerable<dynamic>> GetTop5MedicalRecordsFromToday(int vetId)
        {
            var today = DateTime.Today;
            // Await the task to get the IQueryable result, then apply LINQ
            var AnimalRecords = await _repository.GetMedicalRecordsByVet(vetId);

            var records = AnimalRecords
                .Where(m => m.Date >= today)
                .OrderByDescending(m => m.Date)
                .Take(5).ToList();
                

            return records;
        }

        public async Task<int> GetMedicalRecordsCount(int vetId)
        {
            var records = await _repository.GetMedicalRecordsByVet(vetId);
            return records.Count();
        }


    }
}
