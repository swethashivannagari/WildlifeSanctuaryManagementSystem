using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Repositories;
using static WildlifeSanctuaryManagementSystem.Repositories.AdminRepository;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public class AdminService: IAdminService
    {
        private readonly IAdminRepository _upcomingEvents;
        public AdminService(IAdminRepository upcomingEvents)
        {
            _upcomingEvents = upcomingEvents;
        }

        public async Task<IEnumerable<EventDTO>> GetUpcomingHealthCheckups(){
            return await _upcomingEvents.GetUpcomingHealthCheckups();

        }

        public async Task<DashboardCounts> GetDashboardCounts()
        {
            return await _upcomingEvents.GetDashboardCounts();
        }
    }
}
