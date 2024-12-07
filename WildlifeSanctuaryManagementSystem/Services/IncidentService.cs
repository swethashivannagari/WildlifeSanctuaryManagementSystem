using Microsoft.EntityFrameworkCore;
using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Repositories;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _repository;

        public IncidentService(IIncidentRepository repository)
        {
            _repository = repository;
        }

        // Get incidents reported by a specific user
        public async Task<List<IncidentDto>> GetUserIncidents(int userId)
        {

            var incidents = await _repository.GetUserIncidents(userId);
            return incidents.ToList();
        }

        // Get all incidents
        public async Task<IEnumerable<IncidentDto>> GetAllIncidents()
        {
            return await _repository.GetAllIncidents();
        }

        // Get incident details by IncidentId
        public async Task<IncidentDto> GetByIncidentId(int id)
        {
            return await _repository.GetByIncidentId(id);
        }

        // Add a new incident
        public async Task AddIncident(Incident incident)
        {
            await _repository.AddIncident(incident);
        }

        // Update an existing incident
        public async Task UpdateIncident(Incident incident)
        {
            await _repository.UpdateIncident(incident);
        }

        // Delete an incident by ID
        public async Task DeleteIncident(int id)
        {
            await _repository.DeleteIncidentById(id);
        }

        //count based on servirity
        public async Task<Dictionary<string, int>> GetSeverityCount(int userId)
        {
            var incidents = await _repository.GetUserIncidents(userId);


            return incidents.GroupBy(i => i.Severity).ToDictionary(g => g.Key, g => g.Count());
        }

        //count based on status
        public async Task<Dictionary<string, int>> GetTaskStatusCount(int userId)
        {
            var incidents = await _repository.GetUserIncidents(userId);
            return incidents.GroupBy(i => i.ResolutionStatus).ToDictionary(g => g.Key, g => g.Count());
        }

        // Filter incidents based on severity or resolution status
        public async Task<List<IncidentDto>> FilterIncidents(int userId, string severity = null, string resolutionStatus = null)
        {
                return await _repository.FilterIncidentsAsync(userId, severity, resolutionStatus);
            
        }

        //count of incidents of user
        public async Task<int> GetIncidentsCount(int userId)
        {
            
            var incidents = await _repository.GetUserIncidents(userId);
            return incidents.Count;
        }

        //count sanctuaries related to user
        public async Task<int> GetUniqueSanctuariesCount(int userId)
        {
            Console.WriteLine("username"+userId);
            var incidents = await _repository.GetUserIncidents(userId);
            Console.Write("inc"+incidents);
           
            var uniqueSanctuaries = incidents
                .Select(i => i.SanctuaryId)  
                .Distinct()                  
                .Count();                    

            return uniqueSanctuaries;
        }

        //incidents based on sanctuaries
        public async Task<Dictionary<string, int>> GetIncidentCountBySanctuary()
        {
            return await _repository.GetIncidentCountBySanctuary();
        }
    }
}

