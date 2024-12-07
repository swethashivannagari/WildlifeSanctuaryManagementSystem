using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public interface IReportRepository
    {
        Task<IEnumerable<Report>> GetAllReports(); 
        Task<Report> GetReportById(int id); 
        Task AddReport(Report report);
        Task UpdateReport(Report report); 
        Task DeleteReport(int id); 

    }
}
