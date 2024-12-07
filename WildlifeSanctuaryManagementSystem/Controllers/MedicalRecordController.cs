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
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordService _service;

        public MedicalRecordController(IMedicalRecordService service)
        {
            _service = service;
        }

        //  method to get the current user's ID
        private int? GetUserId()
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            return string.IsNullOrEmpty(userIdClaim) ? null : int.Parse(userIdClaim);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecordsByVet()
        { 
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var userIdString = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            Console.WriteLine("Role" + userRole);
            if (!int.TryParse(userIdString, out int userId))
            {
                return BadRequest("Invalid User ID in claims.");
            }

            if (userRole == "Veterinarian")
            {
                
                var vetRecords = await _service.GetMedicalRecordsByVet(userId);
                return Ok(vetRecords);
            }
            else if (userRole == "Admin" || userRole == "Manager")
            {
                
                var allRecords = await _service.GetAllMedicalRecords();
                return Ok(allRecords);
            }

            return Forbid(); // Role not authorized
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecord>> GetMedicalRecordById(int id)
        {
            try
            {
                var record = await _service.GetMedicalRecordById(id);
                return Ok(record);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Medical record with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Veterinarian")]
        [HttpPost]
        public async Task<ActionResult> AddMedicalRecord(MedicalRecord medicalRecord)
        {
            try
            {
                await _service.AddMedicalRecord(medicalRecord);
                return CreatedAtAction(nameof(GetMedicalRecordById), new { id = medicalRecord.RecordId }, medicalRecord);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Veterinarian")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMedicalRecord(MedicalRecord medicalRecord)
        {

            try
            {
                await _service.UpdateMedicalRecord(medicalRecord);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Medical record  not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Veterinarian")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMedicalRecord(int id)
        {
            try
            {
                await _service.DeleteMedicalRecord(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Medical record with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("schedule")]
        public async Task<IActionResult> GetTop5MedicalRecordsFromToday()
        {
            var vetId = GetUserId();

            if (vetId == null)
            {
                return Unauthorized("User is not authorized or vet ID is missing.");
            }
            var records = await _service.GetTop5MedicalRecordsFromToday(vetId.Value);
            return Ok(records);
        }

        [HttpGet("records/count")]
        public async Task<IActionResult> CountRecords()
        {

            var vetId = GetUserId();

            if (vetId == null)
            {
                return Unauthorized("User is not authorized or vet ID is missing.");
            }
            var count= await _service.GetMedicalRecordsCount(vetId.Value);
            return Ok(count);

        }
    }
}
