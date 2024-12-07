using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public interface IReportService
    {
        Task<IEnumerable<Report>> GetAllReports();
        Task<Report> GetReportById(int id); 
        Task AddReport(Report report);
        Task UpdateReport(Report report); 
        Task DeleteReport(int id); 

    }
}
