using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Services;

namespace WildlifeSanctuaryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourceService _resourceService;

        public ResourcesController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        // GET: api/Resource
        [HttpGet("sanctuary/{id}")]
        public async Task<ActionResult<IEnumerable<Resource>>> GetAllResourcesById(int sanctuaryId)
        {
            var resources = await _resourceService.GetAllResourcesById(sanctuaryId);
            return Ok(resources);
        }

        // GET: api/Resource/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Resource>> GetResourceById(int id)
        {
            var resource = await _resourceService.GetResourceById(id);
            if (resource == null)
            {
                return NotFound($"Resource with ID {id} not found.");
            }
            return Ok(resource);
        }

        // GET: api/Resource/sanctuary/{sanctuaryId}
        [HttpGet("sanctuary/{sanctuaryId}")]
        public async Task<ActionResult<IEnumerable<Resource>>> GetResourcesBySanctuaryId(int sanctuaryId)
        {
            var resources = await _resourceService.GetResourcesBySanctuaryId(sanctuaryId);
            return Ok(resources);
        }

        // POST: api/Resource
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<ActionResult> AddResource([FromBody] Resource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _resourceService.AddResource(resource);
            return CreatedAtAction(nameof(GetResourceById), new { id = resource.ResourceId }, resource);
        }

        // PUT: api/Resource/{id}
        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateResource(int id, [FromBody] Resource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resource.ResourceId)
            {
                return BadRequest("Resource ID mismatch.");
            }

            var existingResource = await _resourceService.GetResourceById(id);
            if (existingResource == null)
            {
                return NotFound($"Resource with ID {id} not found.");
            }

            await _resourceService.UpdateResource(resource);
            return NoContent();
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteResource(int id)
        {
            var resource = await _resourceService.GetResourceById(id);
            if (resource == null)
            {
                return NotFound($"Resource with ID {id} not found.");
            }

            await _resourceService.DeleteResource(id);
            return NoContent();
        }


    }
}
