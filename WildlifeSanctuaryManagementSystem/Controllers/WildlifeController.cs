using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Services;
using static WildlifeSanctuaryManagementSystem.Services.WildlifeService;

namespace WildlifeSanctuaryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WildlifeController : ControllerBase
    {
        private readonly IWildlifeService _service;

        public WildlifeController(IWildlifeService service)
        {
            _service = service;
        }

        // GET: api/WildlifeData
        [Authorize(Roles = "Biologist,Admin,Manager")]
        [HttpGet]
        public async Task<IActionResult> GetWildlifeData()
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var biologistIdString = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            Console.WriteLine("Role" + userRole);


            if (!int.TryParse(biologistIdString, out int biologistId))
            {
                return BadRequest("Invalid Biologist ID in claims.");
            }

            if (userRole == "Biologist")
            {
                
                var wildlifeData = await _service.GetWildlifeDataByBiologist(biologistId);
                return Ok(wildlifeData);
            }
            else if (userRole == "Admin" || userRole == "Manager")
            {
                var allWildlifeData = await _service.GetAllWildlifeData();
                return Ok(allWildlifeData);
            }

            return Forbid();
        }


        // GET: api/WildlifeData/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWildlifeDataById(int id)
        {
            var data = await _service.GetWildlifeDataById(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // GET: api/WildlifeData/sanctuary/{sanctuaryId}
        
        [HttpGet("sanctuary/{sanctuaryId}")]
        public async Task<IActionResult> GetWildlifeDataBySanctuary(int sanctuaryId)
        {
            var data = await _service.GetWildlifeDataBySanctuary(sanctuaryId);
            return Ok(data);
        }


        // POST: api/WildlifeData
        [Authorize(Roles = "Biologist")]
        [HttpPost]
        public async Task<IActionResult> AddWildlifeData(WildlifeData wildlifeData)
        {
            if (wildlifeData == null)
            {
                return BadRequest();
            }
            await _service.AddWildlifeData(wildlifeData);
            return CreatedAtAction(nameof(GetWildlifeDataById), new { id = wildlifeData.DataId }, wildlifeData);
        }
        // PUT: api/WildlifeData/{id}
        [Authorize(Roles = "Biologist,Admin,Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWildlifeData(int id, [FromBody] WildlifeData wildlifeData)
        {
            if (id != wildlifeData.DataId)
            {
                return BadRequest();
            }
            await _service.UpdateWildlifeData(wildlifeData);
            return NoContent();
        }

        // DELETE: api/WildlifeData/{id}
        [Authorize(Roles = "Biologist,Admin,Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWildlifeData(int id)
        {
            var data = await _service.GetWildlifeDataById(id);
            if (data == null)
            {
                return NotFound("Wildlife data record not found.");
            }
            await _service.DeleteWildlifeData(id);
            return NoContent();
        }

        //top observations

        [HttpGet("top-recent-observations")]
        public async Task<ActionResult<List<object>>> GetTopRecentObservations()
        {
            var biologistIdString = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (!int.TryParse(biologistIdString, out int biologistId))
            {
                return BadRequest("Invalid Biologist ID in claims.");
            }
            var topRecentObservations = await _service.GetTopRecentObservationsAsync(biologistId);
            return Ok(topRecentObservations);
        }

        //populationTrends
        [HttpGet("population-trends")]
        public async Task<ActionResult<List<WildlifeData>>> GetPopulationTrends()
        {
            var trends = await _service.GetPopulationTrendsAsync();
            return Ok(trends);
        }
    }
}

    
