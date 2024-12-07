using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Services;

namespace WildlifeSanctuaryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentService _service;

        public IncidentController(IIncidentService service)
        {
            _service = service;
        }

        //  method to get the current user's ID
        private int? GetUserId()
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            return string.IsNullOrEmpty(userIdClaim) ? null : int.Parse(userIdClaim);
        }

        // Get incidents for the logged-in user
        [Authorize(Roles = "Ranger")]
        [HttpGet("User")]
        public async Task<IActionResult> GetUserIncidents()
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized("User is not logged in.");

            var incidents = await _service.GetUserIncidents(userId.Value);
            return Ok(incidents);
        }

        // Get all incidents 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var incidents = await _service.GetAllIncidents();
            return Ok(incidents);
        }

        // Get a specific incident by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var incident = await _service.GetByIncidentId(id);
            if (incident == null)
                return NotFound();

            return Ok(incident);
        }

        [Authorize(Roles = "Ranger")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IncidentDto incidentDto)
        {
            // Validate the incoming model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Get the authenticated user's ID
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            // Map IncidentDto to Incident model
            var incident = new Incident
            {
                SanctuaryId = incidentDto.SanctuaryId,
                Date = incidentDto.Date,
                Description = incidentDto.Description,
                Severity = incidentDto.Severity,
                ResolutionStatus = incidentDto.ResolutionStatus ?? "Unresolved", // Default value if not provided
                ReportedById = userId.Value
            };

            // Add the incident to the database
            await _service.AddIncident(incident);

            // Return the created incident
            return CreatedAtAction(nameof(GetById), new { id = incident.IncidentId }, incident);
        }


        // Update an incident 
        [Authorize(Roles = "Ranger, Admin, Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Incident incident)
        {
            if (id != incident.IncidentId)
                return BadRequest("Incident ID mismatch.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.UpdateIncident(incident);
            return NoContent();
        }

        // Delete an incident 
        [Authorize(Roles = "Ranger,Admin,Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingIncident = await _service.GetByIncidentId(id);
            if (existingIncident == null)
                return NotFound();

            await _service.DeleteIncident(id);
            return NoContent();
        }

        //Get Incident count by Status
        [Authorize(Roles = "Ranger")]
        [HttpGet("StatusCount")]
        public async Task<IActionResult> GetIncidentStatusCount()
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized("User is not logged in.");

            var statusCount = await _service.GetTaskStatusCount(userId.Value);
            return Ok(statusCount);
        }

        //get incidents count based on status
        [Authorize(Roles = "Ranger")]
        [HttpGet("SeverityCount")]
        public async Task<IActionResult> GetIncidentsSeverityCounts()
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized("User is not logged in.");

            var severityCount = await _service.GetSeverityCount(userId.Value);

            return Ok(severityCount);
        }

        [HttpGet("FilterUserIncidents")]
        public async Task<ActionResult<IEnumerable<IncidentDto>>> GetFilteredUserIncidents(
        
        string severity = null,
        string resolutionStatus = null)
            {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized("User is not logged in.");

            if (userId == 0)
            {
                return BadRequest("UserId is required.");
            }

            var incidents = await _service.FilterIncidents(userId.Value, severity, resolutionStatus);
            return Ok(incidents);
        }

        //count of incidents
        [HttpGet("incidents/count")]
        public async Task<IActionResult> GetIncidentsCount()
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized("User is not logged in.");

            var count = await _service.GetIncidentsCount(userId.Value);
            return Ok(count);
        }

        // the count of sanctuaries
        [HttpGet("sanctuaries/count")]
        public async Task<IActionResult> GetSanctuariesCount()
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized("User is not logged in.");

            var count = await _service.GetUniqueSanctuariesCount(userId.Value);
            return Ok(count);
        }

        //get incidents by sanctuary
        [HttpGet("totalIncidentsBySanctuary")]
        public async Task<ActionResult<Dictionary<string, int>>> GetIncidentCountBySanctuary()
        {
            var incidentCounts = await _service.GetIncidentCountBySanctuary();
            return Ok(incidentCounts);
        }

    }
}
