using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Services;

namespace WildlifeSanctuaryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostController : ControllerBase
    {
        private readonly ICostManagementService _service;

        public CostController(ICostManagementService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCosts()
        {
            var costs = await _service.GetAllCosts();
            return Ok(costs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCostById(int id)
        {
            var cost = await _service.GetCostById(id);
            if (cost == null)
            {
                return NotFound("Cost not found.");
            }
            return Ok(cost);
        }

        [Authorize(Roles ="Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> AddCost( CostManagement cost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            cost.Date = DateTime.Now;
            await _service.AddCost(cost);
            return CreatedAtAction(nameof(GetCostById), new { id = cost.CostId }, cost);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCost(int id, [FromBody] CostManagement cost)
        {
            if (id != cost.CostId)
            {
                return BadRequest("Cost ID mismatch.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            cost.Date = DateTime.Now;

            await _service.UpdateCost(cost);
            return NoContent();
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCost(int id)
        {
            var cost = await _service.GetCostById(id);
            if (cost == null)
            {
                return NotFound("Cost not found.");
            }

            await _service.DeleteCost(id);
            return NoContent();



        }

        [HttpGet("total-expenses")]
        public async Task<IActionResult> GetTotalExpensesBySanctuary()
        {
            var totalExpenses =await _service.GetTotalExpensesBySanctuary();
            return Ok(totalExpenses);
        }

        [HttpGet("{sanctuary}/expenses")]
        public async Task<IActionResult> GetExpensesBySanctuaryId(string sanctuary)
        {
            var expenses = await _service.GetExpensesBySanctuary(sanctuary);
            if (expenses == null || !expenses.Any())
            {
                return NotFound("No expenses found for the selected sanctuary.");
            }
            return Ok(expenses);
        }
    }
}