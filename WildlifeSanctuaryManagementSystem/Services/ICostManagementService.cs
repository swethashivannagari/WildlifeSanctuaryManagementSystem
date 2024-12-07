using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Services
{
    public interface ICostManagementService
    {
        Task<IEnumerable<CostManagement>> GetAllCosts();
        Task<CostManagement> GetCostById(int costId);
        Task AddCost(CostManagement cost);
        Task UpdateCost(CostManagement cost);
        Task DeleteCost(int costId);
        Task<Dictionary<string, decimal>> GetTotalExpensesBySanctuary();

        Task<List<CostManagement>> GetExpensesBySanctuary(string sanctuaryName);
    }
}
