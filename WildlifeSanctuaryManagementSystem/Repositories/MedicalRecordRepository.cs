using Microsoft.EntityFrameworkCore;
using WildlifeSanctuaryManagementSystem.Data;
using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public class MedicalRecordRepository:IMedicalRecordRepository
    {
        private readonly SanctuaryDbContext _context;

        public MedicalRecordRepository(SanctuaryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MedicalRecord>> GetAllMedicalRecords()
        {
            return _context.AnimalsMedicalRecords.ToList();
        }

        public async Task<IEnumerable<MedicalRecord>> GetByVetId(int vetId)
        {
            return await _context.AnimalsMedicalRecords
                .Where(m => m.VetId == vetId)
                .Include(m => m.Animal)
                .ToListAsync();
        }

        public async Task<MedicalRecord> GetMedicalRecordById(int id)
        {
            var record = await _context.AnimalsMedicalRecords.FindAsync(id);
            if (record == null)
            {
                throw new KeyNotFoundException($"Medical record with ID {id} not found.");
            }
            return record;
        }

        public async Task AddMedicalRecord(MedicalRecord medicalRecord)
        {
            await _context.AnimalsMedicalRecords.AddAsync(medicalRecord);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMedicalRecord(MedicalRecord medicalRecord)
        {



            _context.AnimalsMedicalRecords.Update(medicalRecord);

            await _context.SaveChangesAsync();
        }


        public async Task DeleteMedicalRecord(int id)
        {
            var medicalRecord = await _context.AnimalsMedicalRecords.FindAsync(id);
            if (medicalRecord == null)
            {
                throw new KeyNotFoundException($"Medical record with ID {id} not found.");
            }

            else
            {
                _context.AnimalsMedicalRecords.Remove(medicalRecord);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<dynamic>> GetMedicalRecordsByVet(int vetId)
        {
            var today = DateTime.UtcNow.Date;
            var records = await _context.AnimalsMedicalRecords
               .Where(r => r.VetId == vetId && r.NextCheckup >= today) 
    .Include(r => r.Animal)
    .OrderBy(r => r.NextCheckup)
    .Select(r => new
    {
        r.RecordId,
        r.Date,
        r.Diagnosis,
        r.Treatment,
        r.NextCheckup,
        r.Animal.Species,
        r.Animal.Age
    })
    .Take(5) // Get only the next 5 records
    .ToListAsync();

            return records;
        }
    }
}
