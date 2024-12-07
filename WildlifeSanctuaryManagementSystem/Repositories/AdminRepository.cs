using Microsoft.EntityFrameworkCore;
using WildlifeSanctuaryManagementSystem.Data;
using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public class AdminRepository: IAdminRepository
    {
        private readonly SanctuaryDbContext _context;

        public AdminRepository(SanctuaryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EventDTO>> GetUpcomingHealthCheckups()
        {
            return await _context.AnimalsMedicalRecords
                .Where(m => m.NextCheckup >= DateTime.Now)
                .Select(m => new EventDTO
                {
                    EventType = "Health Checkup",
                    EventDetail = $"Animal ID: {m.AnimalId}",
                    EventDate = (DateTime)m.NextCheckup
                })
                .OrderBy(e => e.EventDate)
                .Take(3)
                .ToListAsync();
        }

        //resouces and projects

        public async Task<DashboardCounts> GetDashboardCounts()
        {
            var counts = new DashboardCounts
            {
                AnimalCount = await _context.Animals.CountAsync(),
                SanctuaryCount = await _context.Sanctuaries.CountAsync(),
                IncidentCount = await _context.Incidents.CountAsync(),
                UserCount = await _context.Users.CountAsync()
            };

            return counts;
        }

        public class DashboardCounts
        {
            public int AnimalCount { get; set; }
            public int SanctuaryCount { get; set; }
            public int IncidentCount { get; set; }
            public int UserCount { get; set; }
        }
    }
}
