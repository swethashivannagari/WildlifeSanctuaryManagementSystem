using Microsoft.EntityFrameworkCore;
using WildlifeSanctuaryManagementSystem.Data;
using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public class SanctuaryRepository:ISanctuaryRepository
    {
        private readonly SanctuaryDbContext _context;

        public SanctuaryRepository(SanctuaryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sanctuary>> GetAllSanctuaries()
        {
            return await _context.Sanctuaries.ToListAsync(); 
        }

        public async Task<Sanctuary> GetSanctuaryById(int id)
        {
            return await _context.Sanctuaries.FirstOrDefaultAsync(s => s.SanctuaryId == id);
        }

        public async Task AddSanctuary(Sanctuary sanctuary)
        {
            await _context.Sanctuaries.AddAsync(sanctuary);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSanctuary(Sanctuary sanctuary)
        {
            _context.Sanctuaries.Update(sanctuary);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSanctuaryById(int id)
        {
            var sanctuary = await GetSanctuaryById(id);
            if (sanctuary != null)
            {
                _context.Sanctuaries.Remove(sanctuary);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> SanctuaryExists(string name, string location)
        {
            return await _context.Sanctuaries
                .AnyAsync(s => s.Name == name && s.Location == location);
        }



    }
}

