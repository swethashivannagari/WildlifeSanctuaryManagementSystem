using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public interface IIncidentRepository
    {
        Task<IEnumerable<IncidentDto>> GetAllIncidents();
        Task<IncidentDto> GetByIncidentId(int id);
        Task AddIncident(Incident incident);
        Task UpdateIncident(Incident incident);
        Task DeleteIncidentById(int id);
        Task<List<IncidentDto>> GetUserIncidents(int userId);
        public Task<List<IncidentDto>> FilterIncidentsAsync(int userId, string severity = null, string resolutionStatus = null);

        Task<Dictionary<string, int>> GetIncidentCountBySanctuary();
    }
}
