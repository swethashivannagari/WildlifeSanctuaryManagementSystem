using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public interface IAnimalService
    {
        Task<IEnumerable<AnimalDTO>> GetAnimals();
        Task<AnimalDTO> GetAnimalById(int id);
        Task AddAnimal(CreateAnimalDTO dto);
        Task UpdateAnimal(int id, CreateAnimalDTO dto);
        Task DeleteAnimal(int id);
        Task<Dictionary<string, int>> GetAnimalCountByCriteria(string criteria);
        Task<List<string>> GetAnimalsByHealthStatus(string healthStatus);
       Task<Dictionary<string, double>> GetHealthCheckStatus();
    }
}
