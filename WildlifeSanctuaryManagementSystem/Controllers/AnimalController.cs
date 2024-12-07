using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Services;

namespace WildlifeSanctuaryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _service;

        public AnimalController(IAnimalService service)
        {
            _service = service;
        }

        //Get All Animals
        [HttpGet]
        public async Task<IActionResult> GetAnimals()
        {
            try
            {
                var animals = await _service.GetAnimals();
                return Ok(animals);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        //Get Animal By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnimalById(int id)
        {
            try
            {
                var animal = await _service.GetAnimalById(id);
                return Ok(animal);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        //Add Animal
        [HttpPost]
        public async Task<IActionResult> AddAnimal( CreateAnimalDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _service.AddAnimal(dto);
            return CreatedAtAction(nameof(GetAnimalById), new { id = dto.SanctuaryId }, dto);
        }

        //Update Animal
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnimal(int id, CreateAnimalDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _service.UpdateAnimal(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //Delete Animal
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            try
            {
                await _service.DeleteAnimal(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        //get count
        [HttpGet("count")]
        public async Task<IActionResult> GetAnimalCountByCriteria([FromQuery] string criteria)
        {
            if (string.IsNullOrEmpty(criteria))
            {
                return BadRequest("Criteria parameter is required.");
            }

            try
            {
                var result = await _service.GetAnimalCountByCriteria(criteria);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("health/{healthStatus}")]
        public async Task<IActionResult> GetAnimalsByHealthStatus(string healthStatus)
        {
            
            var validHealthStatuses = new[] { "Healthy", "Injured", "Sick", "Critical" };
            if (!validHealthStatuses.Contains(healthStatus, StringComparer.OrdinalIgnoreCase))
            {
                return BadRequest("Health status must be 'Healthy', 'Injured', 'Sick', or 'Critical'.");
            }

           
            var animals = await _service.GetAnimalsByHealthStatus(healthStatus);

            
            if (animals == null || animals.Count == 0)
            {
                return NotFound("No animals found.");
            }

            
            return Ok(animals);
        }

        //percent of health checkup in each sanctuary last month
        [HttpGet("checkup-percent")]
        public async Task<ActionResult<Dictionary<string, double>>> GetHealthCheckStatus()
        {
            var healthCheckStatus = await _service.GetHealthCheckStatus();
            return Ok(healthCheckStatus);
        }
    }
}
