using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WildlifeSanctuaryManagementSystem.Services;

namespace WildlifeSanctuaryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IAdminService _eventService;
        public EventsController(IAdminService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcomingEvents()
        {
            try
            {
                var events = await _eventService.GetUpcomingHealthCheckups();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("counts")]
        public async Task<IActionResult> GetDashboardCounts()
        {
            var dashboardCounts = await _eventService.GetDashboardCounts();
            return Ok(dashboardCounts);
        }
    }
}
