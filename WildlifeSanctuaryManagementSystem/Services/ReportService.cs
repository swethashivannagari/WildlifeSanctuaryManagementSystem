using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Repositories;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public class ReportService:IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<IEnumerable<Report>> GetAllReports()
        {
            return await _reportRepository.GetAllReports();
        }
        public async Task<Report> GetReportById(int id)
        {
            return await _reportRepository.GetReportById(id);
        }

        public async Task AddReport(Report report)
        {
            await _reportRepository.AddReport(report);
        }

        public async Task UpdateReport(Report report)
        {
            await _reportRepository.UpdateReport(report);
        }

        public async Task DeleteReport(int id)
        {
            await _reportRepository.DeleteReport(id);
        }
    }
}
