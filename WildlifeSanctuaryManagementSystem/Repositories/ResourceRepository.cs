using Microsoft.EntityFrameworkCore;
using WildlifeSanctuaryManagementSystem.Data;
using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Services;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly SanctuaryDbContext _context;
        private readonly ISanctuaryService _sanctuaryService;
        private const int LowStockThreshold = 5;  // Set the threshold for low stock notification

        public ResourceRepository(SanctuaryDbContext context,ISanctuaryService sanctuaryService)
        {
            _context = context;
            _sanctuaryService = sanctuaryService;
        }

        public async Task<IEnumerable<Resource>> GetAllResourcesById(int sanctuaryId)
        {
            return await _context.Resources
                .Where(r => r.SanctuaryId == sanctuaryId)
                .Include(r => r.Sanctuary)
                .ToListAsync();
        }

        public async Task<Resource> GetResourceById(int id)
        {
            return await _context.Resources
                .Include(r => r.Sanctuary)
                .FirstOrDefaultAsync(r => r.ResourceId == id);
        }

        public async Task<IEnumerable<Resource>> GetResourcesBySanctuaryId(int sanctuaryId)
        {
            return await _context.Resources
                .Include(r => r.Sanctuary)
                .Where(r => r.SanctuaryId == sanctuaryId)
                .ToListAsync();
        }

        public async Task AddResource(Resource resource)
        {
            await _context.Resources.AddAsync(resource);
            await _context.SaveChangesAsync();
            await CheckResourceQuantityAndNotify(resource);
        }

        public async Task UpdateResource(Resource resource)
        {
            _context.Resources.Update(resource);
            await _context.SaveChangesAsync();
            await CheckResourceQuantityAndNotify(resource);
        }

        public async Task DeleteResource(int id)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource != null)
            {
                _context.Resources.Remove(resource);
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckResourceQuantityAndNotify(Resource resource)
        {
            var managerId = await GetManagerIdBySanctuaryId(resource.SanctuaryId);
            if (resource.Quantity <= LowStockThreshold)
            {
                var notification = new Notification
                {
                    Type="Resources",
                    Message = $"Warning: The resource '{resource.Type}' is running low. Only {resource.Quantity} left.",
                    UserId = managerId

                };

                await _context.Notifications.AddAsync(notification);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<int> GetManagerIdBySanctuaryId(int sanctuaryId)
        {
            var sanctuary = await _context.Sanctuaries
                .Where(s => s.SanctuaryId == sanctuaryId)
                .FirstOrDefaultAsync();

            if (sanctuary != null)
            {
                return sanctuary.ManagerId;  
            }

            return 0;  
        }
    }
}
