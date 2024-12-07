using WildlifeSanctuaryManagementSystem.Models;
using static WildlifeSanctuaryManagementSystem.Repositories.AdminRepository;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public interface IAdminService
    {
        Task<IEnumerable<EventDTO>> GetUpcomingHealthCheckups();
        Task<DashboardCounts> GetDashboardCounts();

    }
}
