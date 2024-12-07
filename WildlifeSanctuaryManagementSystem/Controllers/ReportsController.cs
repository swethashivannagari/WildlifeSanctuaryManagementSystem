using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Services;

namespace WildlifeSanctuaryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetReports()
        {
            var reports = await _reportService.GetAllReports();
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReport(int id)
        {
            var report = await _reportService.GetReportById(id);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }

        [HttpPost]
        public async Task<ActionResult> CreateReport(Report report)
        {
            report.GeneratedDate = DateTime.Now;
            await _reportService.AddReport(report);
            return CreatedAtAction(nameof(GetReport), new { id = report.ReportId }, report);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(int id, Report report)
        {
            if (id != report.ReportId)
            {
                return BadRequest();
            }

            report.GeneratedDate = DateTime.Now;
            await _reportService.UpdateReport(report);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            await _reportService.DeleteReport(id);
            return NoContent();
        }

    }
}
