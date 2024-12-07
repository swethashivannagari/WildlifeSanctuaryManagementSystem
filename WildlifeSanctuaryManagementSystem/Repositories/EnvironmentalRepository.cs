using Microsoft.EntityFrameworkCore;
using WildlifeSanctuaryManagementSystem.Data;
using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public class EnvironmentalRepository:IEnvironmentalRepository
    {
        private readonly SanctuaryDbContext _context;

        public EnvironmentalRepository(SanctuaryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EnvironmentalData>> GetAllData()
        {
            return await _context.EnvironmentalData
                .ToListAsync();
        }

        public async Task<IEnumerable<EnvironmentalData>> GetEnvironmentalDataByBiologistId(int biologistId)
        {
            return await _context.EnvironmentalData
                .Where(e => e.ConductedBy == biologistId)
                .ToListAsync();
        }

        public async Task<EnvironmentalData> GetDataById(int id)
        {
            return await _context.EnvironmentalData
                .FirstOrDefaultAsync(ed => ed.AssessmentId == id);
        }

        public async Task<IEnumerable<EnvironmentalData>> GetByConductedBy(int conductedById)
        {
            return await _context.EnvironmentalData
                
                .Where(ed => ed.ConductedBy == conductedById)
                .ToListAsync();
        }
        public async Task AddData(EnvironmentalData environmentalData)
        {
            await _context.EnvironmentalData.AddAsync(environmentalData);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateData(EnvironmentalData environmentalData)
        {
            _context.EnvironmentalData.Update(environmentalData);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteData(int id)
        {
            var environmentalData = await _context.EnvironmentalData.FindAsync(id);
            if (environmentalData != null)
            {
                _context.EnvironmentalData.Remove(environmentalData);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Object>> GetAssessmentsBySanctuary()
        {
            return await _context.EnvironmentalData
                .GroupBy(a => a.Sanctuary.Name)
                .Select(g => new
                {
                    Sanctuary = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Object>> GetAssessmentsByImpactType()
        {
            return await _context.EnvironmentalData
                .GroupBy(a => a.ImpactType)
                .Select(g => new 
                {
                    ImpactType = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();
        }
    }
}
