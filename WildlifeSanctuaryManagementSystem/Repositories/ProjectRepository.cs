using Microsoft.EntityFrameworkCore;
using WildlifeSanctuaryManagementSystem.Data;
using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public class ProjectRepository:IProjectRepository
    {
        private readonly SanctuaryDbContext _context;

        public ProjectRepository(SanctuaryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _context.Projects
                .ToListAsync();
        }

        public async Task<Project> GetProjectById(int id)
        {
            return await _context.Projects
                 .FirstOrDefaultAsync(p => p.ProjectId == id);
        }
        public async Task AddProject(Project project)
        {
           //ValidateRangerRole(project);
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            var notification = new Notification
            {
                UserId = project.RangerId, 
                Message = $"A new project '{project.ActivityType}' has been assigned to you.",
                Timestamp = DateTime.Now
            };

            _context.Notifications.Add(notification);

        }

        public async Task UpdateProject(Project project)
        {
            await ValidateRangerRole(project);
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }
         private async Task ValidateRangerRole(Project project)
        {
            var ranger = _context.Users.FirstOrDefault(u => u.UserId == project.RangerId && u.Role == "Ranger");
            if (ranger == null)
            {
                throw new Exception("The assigned ranger must have the role 'Ranger'.");
            }
        }

    }
}
