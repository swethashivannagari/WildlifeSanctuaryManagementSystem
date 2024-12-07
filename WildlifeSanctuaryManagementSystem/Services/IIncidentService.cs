using System.Collections.Generic;
using System.Threading.Tasks;
using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public interface IIncidentService
    {
        // Get incidents reported by a specific user
        Task<List<IncidentDto>> GetUserIncidents(int userId);

        // Get all incidents
        Task<IEnumerable<IncidentDto>> GetAllIncidents();

        // Get incident details by IncidentId
        Task<IncidentDto> GetByIncidentId(int id);

        // Add a new incident
        Task AddIncident(Incident incident);

        // Update an existing incident
        Task UpdateIncident(Incident incident);

       

        // Delete an incident by ID
        Task DeleteIncident(int id);

        // Get count of incidents grouped by severity
        Task<Dictionary<string, int>> GetSeverityCount(int userId);

        // Get count of incidents grouped by task status
        Task<Dictionary<string, int>> GetTaskStatusCount(int userId);

        // Filter incidents based on severity or resolution status
        public Task<List<IncidentDto>> FilterIncidents(int userId, string severity = null, string resolutionStatus = null);

        public  Task<int> GetUniqueSanctuariesCount(int userId);
        public  Task<int> GetIncidentsCount(int userId);

        Task<Dictionary<string, int>> GetIncidentCountBySanctuary();



    }
}
