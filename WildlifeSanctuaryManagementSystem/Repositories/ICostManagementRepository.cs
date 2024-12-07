using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public interface ICostManagementRepository
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
