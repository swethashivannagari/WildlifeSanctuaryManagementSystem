using WildlifeSanctuaryManagementSystem.Models;
using static WildlifeSanctuaryManagementSystem.Repositories.AdminRepository;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public interface IAdminRepository
    {
         Task<IEnumerable<EventDTO>> GetUpcomingHealthCheckups();
        Task<DashboardCounts> GetDashboardCounts();
    }
}
