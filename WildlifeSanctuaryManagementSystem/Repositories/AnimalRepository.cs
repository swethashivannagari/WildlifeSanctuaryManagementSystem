using Microsoft.EntityFrameworkCore;
using WildlifeSanctuaryManagementSystem.Data;
using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public class AnimalRepository:IAnimalRepository
    {
        private readonly SanctuaryDbContext _context;

        public AnimalRepository(SanctuaryDbContext context)
        {
            _context = context;
        }

        //get all animals
        public async Task<IEnumerable<Animal>> GetAnimals()
        {
            return await _context.Animals.Include(a => a.Sanctuary).ToListAsync();
        }

        //get animal by id
        public async Task<Animal> GetAnimalById(int id)
        {
            var animal = await _context.Animals.Include(a => a.Sanctuary).FirstOrDefaultAsync(a => a.AnimalId == id);

            return animal;
        }

        //create animal
        public async Task AddAnimal(Animal animal)
        {
            await _context.Animals.AddAsync(animal);
           
            await _context.SaveChangesAsync();
            await CheckAndNotifyCriticalAnimalHealthStatus(animal);
        }

        //update animal
        public async Task UpdateAnimal(Animal animal)
        {
            _context.Animals.Update(animal);
            
            await _context.SaveChangesAsync();
            await CheckAndNotifyCriticalAnimalHealthStatus(animal);
        }

        //delete animal by id
        public async Task DeleteAnimal(int id)
        {
            var animal = await GetAnimalById(id);
            if (animal == null)
            {

                throw new Exception($"Animal with ID {id} does not exist.");
               
            }
            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();

        }

        public async Task<Dictionary<string, double>> GetHealthCheckStatus()
        {
            var healthCheckStatus = await _context.Animals
                .GroupBy(a => a.CurrentLocation)
                .Select(group => new
                {
                    SanctuaryName = group.Key,
                    CheckupPercentage = group.Count(a => a.LastCheckupDate >= DateTime.UtcNow.AddDays(-30)) * 100.0 / group.Count()
                })
                .ToListAsync();

            
            return healthCheckStatus.ToDictionary(x => x.SanctuaryName, x => x.CheckupPercentage);
        }


        //create notification
        public async Task CheckAndNotifyCriticalAnimalHealthStatus(Animal animal)
        {

            if (animal.HealthStatus == "Critical")
            {
                var managerId = await GetManagerIdBySanctuaryId(animal.SanctuaryId);
                var notification = new Notification
                {
                    Type = "Medical Emergency",
                    Message = $"Urgent: Animal '{animal.Species}' (ID: {animal.AnimalId}) is in critical condition. Immediate attention required!",
                    Timestamp = DateTime.UtcNow,
                    UserId = managerId
                };
                

                await _context.Notifications.AddAsync(notification);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<int> GetManagerIdBySanctuaryId(int sanctuaryId)
        {
            var sanctuary = await _context.Sanctuaries
                .Where(s => s.SanctuaryId == sanctuaryId)
                .FirstOrDefaultAsync();

            if (sanctuary != null)
            {
                return sanctuary.ManagerId;
            }

            return 0;
        }



    }
}
