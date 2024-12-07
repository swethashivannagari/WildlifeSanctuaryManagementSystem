using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Repositories;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;

        public ProjectService(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _repository.GetAllProjects();
        }

        public async Task<Project> GetProjectById(int id)
        {
            return await _repository.GetProjectById(id);
        }

        public async Task AddProject(Project project)
        {
            ValidateProjectDates(project);

            await _repository.AddProject(project);
        }

        public async Task UpdateProject(Project project)
        {
            ValidateProjectDates(project);
            await _repository.UpdateProject(project);
        }

        public async Task DeleteProject(int id)
        {
            await _repository.DeleteProject(id);
        }

        private void ValidateProjectDates(Project project)
        {
            if (project.EndDate <= project.StartDate)
            {
                throw new Exception("End date must be later than the start date.");
            }
        }


    }
}
