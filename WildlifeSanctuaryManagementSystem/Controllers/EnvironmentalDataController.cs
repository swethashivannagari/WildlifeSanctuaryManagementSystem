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
    public class EnvironmentalDataController : ControllerBase
    {
        private readonly IEnvironmentalService _service;

        public EnvironmentalDataController(IEnvironmentalService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Conservationist,Admin,Manager")]
        [HttpGet]
        public async Task<IActionResult> GetEnvironmentalData()
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var userIdString = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            Console.WriteLine("Role" + userRole);

            if (!int.TryParse(userIdString, out int userId))
            {
                return BadRequest("Invalid User ID in claims.");
            }

            if (userRole == "Admin" || userRole == "Manager")
            {
                
                var allEnvironmentalData = await _service.GetAllData();
                return Ok(allEnvironmentalData);
            }
            else if (userRole == "Conservationist")
            {
                
                var environmentalData = await _service.GetEnvironmentalDataByBiologistId(userId);
                return Ok(environmentalData);
            }

            return Forbid(); 
        }

        // GET: api/EnvironmentalData/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDataById(int id)
        {
            var data = await _service.GetDataById(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // GET: api/EnvironmentalData/conductedby/{conductedById}
        [Authorize(Roles = "Admin,Manager,Coservationist")]
        [HttpGet("conductedby/{conductedById}")]
        public async Task<IActionResult> GetByConductedBy(int conductedById)
        {
            var data = await _service.GetByConductedBy(conductedById);
            return Ok(data);
        }

        [Authorize(Roles = "Admin,Manager,Conservationist")]
        // POST: api/EnvironmentalData
        [HttpPost]
        public async Task<IActionResult> AddData(EnvironmentalData environmentalData)
        {
            if (environmentalData == null)
            {
                return BadRequest();
            }
            await _service.AddData(environmentalData);
            return CreatedAtAction(nameof(GetDataById), new { id = environmentalData.AssessmentId }, environmentalData);
        }

        [Authorize(Roles = "Admin,Manager,Coservationist")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateData(int id, [FromBody] EnvironmentalData environmentalData)
        {
            if (id != environmentalData.AssessmentId)
            {
                return BadRequest();
            }
            await _service.UpdateData(environmentalData);
            return NoContent();
        }

        // DELETE: api/EnvironmentalData/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var data = await _service.GetDataById(id);
            if (data == null)
            {
                return NotFound("data record not found.");
            }
            await _service.DeleteData(id);
            return NoContent();
        }

        // GET: api/assessment/by-sanctuary
        [HttpGet("by-sanctuary")]
        public async Task<IActionResult> GetAssessmentsBySanctuary()
        {
            var result = await _service.GetAssessmentsBySanctuary();
            return Ok(result);
        }

        // GET: api/assessment/by-impact-type
        [HttpGet("by-impact-type")]
        public async Task<IActionResult> GetAssessmentsByImpactType()
        {
            var result = await _service.GetAssessmentsByImpactType();
            return Ok(result);
        }
    }
}
