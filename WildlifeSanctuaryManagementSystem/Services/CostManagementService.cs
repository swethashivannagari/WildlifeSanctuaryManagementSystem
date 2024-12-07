using WildlifeSanctuaryManagementSystem.Models;
using WildlifeSanctuaryManagementSystem.Repositories;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public class CostManagementService:ICostManagementService
    {
        private readonly ICostManagementRepository _repository;

        public CostManagementService(ICostManagementRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CostManagement>> GetAllCosts()
        {
            return await _repository.GetAllCosts();
        }

        public async Task<CostManagement> GetCostById(int costId)
        {
            return await _repository.GetCostById(costId);
        }

        public async Task AddCost(CostManagement cost)
        {
            await _repository.AddCost(cost);
        }

        public async Task UpdateCost(CostManagement cost)
        {
            await _repository.UpdateCost(cost);
        }

        public async Task<List<CostManagement>> GetExpensesBySanctuary(string sanctuaryName)
        {
            return await _repository.GetExpensesBySanctuary(sanctuaryName);
        }

        public async Task DeleteCost(int costId)
        {
            await _repository.DeleteCost(costId);
        }

        public async Task<Dictionary<string, decimal>> GetTotalExpensesBySanctuary()
        {
            return await _repository.GetTotalExpensesBySanctuary();
        }
    }
}
