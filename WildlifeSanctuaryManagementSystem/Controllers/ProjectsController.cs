using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Services;

namespace WildlifeSanctuaryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _service;

        public ProjectsController(IProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
        {
            var projects = await _service.GetAllProjects();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            var project = await _service.GetProjectById(id);
            if (project == null)
            {
                return NotFound(new { Message = "Project not found." });
            }
            return Ok(project);
        }

       // [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<ActionResult> AddProject(Project project)
        {
            try
            {
                await _service.AddProject(project);
                return CreatedAtAction(nameof(GetProjectById), new { id = project.ProjectId }, project);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,Manager,Ranger")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProject(int id, Project project)
        {
            if (id != project.ProjectId)
            {
                return BadRequest(new { Message = "Project ID mismatch." });
            }

            try
            {
                await _service.UpdateProject(project);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(int id)
        {
            try
            {
                await _service.DeleteProject(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
