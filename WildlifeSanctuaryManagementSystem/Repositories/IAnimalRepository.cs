using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public interface IAnimalRepository
    {
        Task<IEnumerable<Animal>> GetAnimals();
        Task<Animal> GetAnimalById(int id);
        Task AddAnimal(Animal animal);
        Task UpdateAnimal(Animal animal);
        Task DeleteAnimal(int id);
        Task<Dictionary<string, double>> GetHealthCheckStatus();
    }
}
