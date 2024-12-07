using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Services;

namespace WildlifeSanctuaryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanctuaryController : ControllerBase
    {
        private readonly ISanctuaryService _sanctuaryService;

        public SanctuaryController(ISanctuaryService _sanctuaryService)
        {
            this._sanctuaryService = _sanctuaryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sanctuary>>> GetAllSanctuaries()
        {
            var sanctuaries = await _sanctuaryService.GetAllSanctuaries();
            return Ok(sanctuaries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sanctuary>> GetSanctuaryById(int id)
        {
            var sanctuary = await _sanctuaryService.GetSanctuaryById(id);
            if (sanctuary == null)
                return NotFound();
            return Ok(sanctuary);
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpPost]
        public async Task<IActionResult> AddSanctuary(Sanctuary sanctuary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var managerId = User.FindFirst("userId")?.Value;

            if (string.IsNullOrEmpty(managerId))
            {
                return Unauthorized();
            }

            sanctuary.ManagerId = int.Parse(managerId);

            try
            {
                // Check if a sanctuary with the same name and location exists
                var exists = await _sanctuaryService.CheckSanctuaryExists(sanctuary.Name, sanctuary.Location);
                if (exists)
                {
                    return Conflict(new { message = "Sanctuary with the same name and location already exists." });
                }

                await _sanctuaryService.AddSanctuary(sanctuary);
                return CreatedAtAction(nameof(GetSanctuaryById), new { id = sanctuary.SanctuaryId }, sanctuary);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while adding the sanctuary.", error = ex.Message });
            }
        }


        [Authorize(Roles = "Manager, Admin")] 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSanctuary(int id, [FromBody] Sanctuary sanctuary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != sanctuary.SanctuaryId)
                return BadRequest();

            await _sanctuaryService.UpdateSanctuary(sanctuary);
            return NoContent();
        }

        [Authorize(Roles ="Admin,Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteSanctuary(int id)
        {
            await _sanctuaryService.DeleteSanctuaryById(id);
            return NoContent();
        }

        [HttpGet("sanctuaries")]
        public async Task<IActionResult> GetSanctuariesWithIds()
        {
            var sanctuaries = await _sanctuaryService.GetSanctuariesWithIds();
            return Ok(sanctuaries);
        }
    }
}
