using Microsoft.EntityFrameworkCore;
using WildlifeSanctuaryManagementSystem.Data;
using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public class WildlifeRepository: IWildlifeRepository

    {
            private readonly SanctuaryDbContext _dbContext;

            public WildlifeRepository(SanctuaryDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            // Get all wildlife data
            public async Task<IEnumerable<WildlifeData>> GetAllWildlifeData()
            {
                return await _dbContext.WildlifeData
                    .Include(wd => wd.Sanctuary) 
                   
                    .ToListAsync();
            }

        public async Task<IEnumerable<WildlifeData>> GetWildlifeDataByBiologistId(int biologistId)
        {
            return await _dbContext.WildlifeData
                .Where(w => w.BiologistId == biologistId)
                .Include(w => w.Sanctuary)
                .ToListAsync();
        }


        // Get wildlife data by ID
        public async Task<WildlifeData> GetWildlifeDataById(int dataId)
            {
                return await _dbContext.WildlifeData
                    .Include(wd => wd.Sanctuary)
                   // .Include(wd => wd.Biologist)
                    .FirstOrDefaultAsync(wd => wd.DataId == dataId);
            }

            // Add new wildlife data
            public async Task AddWildlifeData(WildlifeData wildlifeData)
            {
                await _dbContext.WildlifeData.AddAsync(wildlifeData);
                await _dbContext.SaveChangesAsync();
            }

            // Update existing wildlife data
            public async Task UpdateWildlifeData(WildlifeData wildlifeData)
            {
                _dbContext.WildlifeData.Update(wildlifeData);
                await _dbContext.SaveChangesAsync();
            }

            // Delete wildlife data
            public async Task DeleteWildlifeData(int dataId)
            {
                var wildlifeData = await GetWildlifeDataById(dataId);
                if (wildlifeData != null)
                {
                    _dbContext.WildlifeData.Remove(wildlifeData);
                    await _dbContext.SaveChangesAsync();
                }
            }

            // Get wildlife data by sanctuary
            public async Task<List<WildlifeData>> GetWildlifeDataBySanctuary(int sanctuaryId)
            {
                return await _dbContext.WildlifeData
                    .Where(wd => wd.SanctuaryId == sanctuaryId)
                    .Include(wd => wd.Sanctuary)
                    .ToListAsync();
            }

            

        //get population trend
        public async Task<List<WildlifeData>> GetPopulationTrendsAsync()
        {
            return await _dbContext.WildlifeData
                .GroupBy(w => w.Species)
                .Select(g => new WildlifeData
                {
                    Species = g.Key,
                    PopulationEstimate = (int?)g.Average(x => x.PopulationEstimate)
                })
                .ToListAsync();
        }

        //recent Observations
        public async Task<IEnumerable<object>> GetTopRecentObservationsAsync(int id)
        {
            return await _dbContext.WildlifeData
                .Where(w => w.BiologistId == id)
                .OrderByDescending(w => w.ObservationDate)
                .Take(5)
                .Select(w => new
                {
                   Species= w.Species,
                   ObservationDate= w.ObservationDate
                })
                .ToListAsync();
        }

    }
}


