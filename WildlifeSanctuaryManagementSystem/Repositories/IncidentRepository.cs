using Microsoft.EntityFrameworkCore;
using WildlifeSanctuaryManagementSystem.Data;
using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly SanctuaryDbContext _context;
        public IncidentRepository(SanctuaryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IncidentDto>> GetAllIncidents()
        {
            return await _context.Incidents
                .Select(i => new IncidentDto
                {
                    IncidentId = i.IncidentId,
                    SanctuaryId = i.SanctuaryId,
                    Date = i.Date,
                    Description = i.Description,
                    Severity = i.Severity,
                    ResolutionStatus = i.ResolutionStatus,
                    ReportedById = i.ReportedById
                })
                .ToListAsync();
        }

        public async Task<IncidentDto> GetByIncidentId(int id)
        {
            var incident = await _context.Incidents
                .Where(i => i.IncidentId == id)
                .Select(i => new IncidentDto
                {
                    IncidentId = i.IncidentId,
                    SanctuaryId = i.SanctuaryId,
                    Date = i.Date,
                    Description = i.Description,
                    Severity = i.Severity,
                    ResolutionStatus = i.ResolutionStatus,
                    ReportedById = i.ReportedById
                })
                .FirstOrDefaultAsync();

            return incident;
        }

        public async Task AddIncident(Incident incident)
        {
            await _context.Incidents.AddAsync(incident);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateIncident(Incident incident)
        {
            _context.Incidents.Update(incident);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIncidentById(int id)
        {
            var incident = await _context.Incidents
        .FirstOrDefaultAsync(i => i.IncidentId == id);

            if (incident != null)
            {
                _context.Incidents.Remove(incident);
                await _context.SaveChangesAsync();
            }
            }

        public async Task<List<IncidentDto>> GetUserIncidents(int userId)
        {
            
            return await _context.Incidents
                .Where(i => i.ReportedById == userId)
                .Select(i => new IncidentDto
                {
                    IncidentId = i.IncidentId,
                    SanctuaryId = i.SanctuaryId,
                    Date = i.Date,
                    Description = i.Description,
                    Severity = i.Severity,
                    ResolutionStatus = i.ResolutionStatus,
                    ReportedById = i.ReportedById,
                    SanctuaryName=i.Sanctuary.Name
                })
                .ToListAsync();
        }

        public async Task<List<IncidentDto>> FilterIncidentsAsync(int userId, string severity = null, string resolutionStatus = null)
        {
            var incidentsQuery = _context.Incidents.AsQueryable();

            // Filter by userId
            incidentsQuery = incidentsQuery.Where(i => i.ReportedById == userId);

            // Filter by severity if provided
            if (!string.IsNullOrEmpty(severity))
            {
                incidentsQuery = incidentsQuery.Where(i => i.Severity == severity);
            }

            // Filter by resolution status if provided
            if (!string.IsNullOrEmpty(resolutionStatus))
            {
                incidentsQuery = incidentsQuery.Where(i => i.ResolutionStatus == resolutionStatus);
            }

            // Return filtered incidents
            return await incidentsQuery
                .Select(i => new IncidentDto
                {
                    IncidentId = i.IncidentId,
                    SanctuaryId = i.SanctuaryId,
                    Date = i.Date,
                    Description = i.Description,
                    Severity = i.Severity,
                    ResolutionStatus = i.ResolutionStatus,
                    ReportedById = i.ReportedById,
                    SanctuaryName = i.Sanctuary.Name
                })
                .ToListAsync();
        }

        public async Task<Dictionary<string, int>> GetIncidentCountBySanctuary()
        {
            var incidentCounts = await _context.Incidents
                .Include(i => i.Sanctuary) 
                .GroupBy(i => i.Sanctuary.Name) 
                .Select(group => new
                {
                    SanctuaryName = group.Key,
                    IncidentCount = group.Count()
                })
                .ToListAsync();

            return incidentCounts.ToDictionary(x => x.SanctuaryName, x => x.IncidentCount);
        }

    }
}
