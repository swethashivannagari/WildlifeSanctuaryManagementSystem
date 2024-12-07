using Microsoft.EntityFrameworkCore;
using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Repositories;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public class AnimalService:IAnimalService
    {
        private readonly IAnimalRepository _repository;

        public AnimalService(IAnimalRepository repository)
        {
            _repository = repository;
        }

        //Get all animals
        public async Task<IEnumerable<AnimalDTO>> GetAnimals()
        {
            var animals = await _repository.GetAnimals();
            return animals.Select(a => new AnimalDTO
            {
                AnimalId = a.AnimalId,
                Species = a.Species,
                Age = a.Age,
                Gender = a.Gender,
                HealthStatus = a.HealthStatus,
                CurrentLocation = a.CurrentLocation,
                LastCheckupDate = a.LastCheckupDate,
                SanctuaryId = a.SanctuaryId,
                SanctuaryName = a.Sanctuary.Name,
            });
        }

        //Get animal by id
        public async Task<AnimalDTO> GetAnimalById(int id)
        {
            var animal = await _repository.GetAnimalById(id);
            if (animal == null)
                throw new Exception($"Animal with ID {id} not found.");

            return new AnimalDTO
            {
                AnimalId = animal.AnimalId,
                Species = animal.Species,
                Age = animal.Age,
                Gender = animal.Gender,
                HealthStatus = animal.HealthStatus,
                CurrentLocation = animal.CurrentLocation,
                LastCheckupDate = animal.LastCheckupDate,
                SanctuaryId = animal.SanctuaryId,
                SanctuaryName = animal.Sanctuary.Name
            };
        }

        //add animal
        public async Task AddAnimal(CreateAnimalDTO animaldto)
        {
            var animal = new Animal
            {
                Species = animaldto.Species,
                Age = animaldto.Age,
                Gender = animaldto.Gender,
                HealthStatus = animaldto.HealthStatus,
                CurrentLocation = animaldto.CurrentLocation,
                LastCheckupDate = animaldto.LastCheckupDate,
                SanctuaryId = animaldto.SanctuaryId
            };
            await _repository.AddAnimal(animal);
        }

        //update animal
        public async Task UpdateAnimal(int id, CreateAnimalDTO dto)
        {
            var animal = await _repository.GetAnimalById(id);
            if (animal == null) throw new Exception("Animal not found");

            animal.Species = dto.Species;
            animal.Age = dto.Age;
            animal.Gender = dto.Gender;
            animal.HealthStatus = dto.HealthStatus;
            animal.CurrentLocation = dto.CurrentLocation;
            animal.LastCheckupDate = dto.LastCheckupDate;
            animal.SanctuaryId = dto.SanctuaryId;

            await _repository.UpdateAnimal(animal);
        }

        //delete animal
        public async Task DeleteAnimal(int id)
        {
            var animal = await _repository.GetAnimalById(id);
            if (animal == null)
                throw new Exception($"Animal with ID {id} not found.");

            await _repository.DeleteAnimal(id);
        }

        //count animals by severity
        public async Task<Dictionary<string, int>> GetAnimalCountByCriteria(string criteria)
        {
            var animals = await _repository.GetAnimals();

            
            Dictionary<string, int> result = new Dictionary<string, int>();

           

            if (criteria.ToLower() == "health")
            {
                
                result = animals
                    .GroupBy(a => a.HealthStatus)
                    .ToDictionary(g => g.Key, g => g.Count());
            }
            else if (criteria.ToLower() == "age")
            {
                
                result = animals
                    .GroupBy(a => GetAgeRange(a.Age))
                    .ToDictionary(g => g.Key, g => g.Count());
            }
            else
            {
                throw new Exception("Invalid criteria. Please specify 'health' or 'age'.");
            }

            return result;
        }

        public async Task<List<string>> GetAnimalsByHealthStatus(string healthStatus)
        {
            
            var animals = await _repository.GetAnimals();

            
            var filteredAnimals = animals
                .Where(a => a.HealthStatus.Equals(healthStatus, StringComparison.OrdinalIgnoreCase))
                .Select(a=>a.Species)
                .ToList();

            return filteredAnimals;
        }

        public async Task<Dictionary<string, double>> GetHealthCheckStatus()
        {
            return await _repository.GetHealthCheckStatus();  
        }

       
       

        //helper method
        private string GetAgeRange(int age)
        {
            if (age < 1)
                return "Baby";
            else if (age >= 1 && age < 5)
                return "Young";
            else if (age >= 5 && age < 10)
                return "Adult";
            else
                return "Senior";
        }

        
    }

}
