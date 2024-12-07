using Microsoft.EntityFrameworkCore;
using WildlifeSanctuaryManagementSystem.Data;
using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public class ReportRepository:IReportRepository
    {
        private readonly SanctuaryDbContext _context;

        public ReportRepository(SanctuaryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Report>> GetAllReports()
        {
            return await _context.Reports.ToListAsync();
        }
        public async Task<Report> GetReportById(int id)
        {
            return await _context.Reports.FirstOrDefaultAsync(r => r.ReportId == id);
        }

        public async Task AddReport(Report report)
        {
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReport(Report report)
        {
            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteReport(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report != null)
            {
                _context.Reports.Remove(report);
                await _context.SaveChangesAsync();
            }
        }
    }
}
