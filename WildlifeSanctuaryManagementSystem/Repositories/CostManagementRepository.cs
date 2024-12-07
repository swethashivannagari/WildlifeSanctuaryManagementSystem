using Microsoft.EntityFrameworkCore;
using WildlifeSanctuaryManagementSystem.Data;
using WildlifeSanctuaryManagementSystem.Models;

namespace WildlifeSanctuaryManagementSystem.Repositories
{
    public class CostManagementRepository:ICostManagementRepository
    {
        private readonly SanctuaryDbContext _dbContext;

        public CostManagementRepository(SanctuaryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CostManagement>> GetAllCosts()
        {
            return await _dbContext.CostManagements.ToListAsync();
        }

        public async Task<CostManagement> GetCostById(int costId)
        {
            return await _dbContext.CostManagements.FindAsync(costId);
        }

        public async Task AddCost(CostManagement cost)
        {
            await _dbContext.CostManagements.AddAsync(cost);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCost(CostManagement cost)
        {
            _dbContext.CostManagements.Update(cost);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCost(int costId)
        {
            var cost = await GetCostById(costId);
            if (cost != null)
            {
                _dbContext.CostManagements.Remove(cost);
                await _dbContext.SaveChangesAsync();
            }
        }

        //get expenses based on sanctuary
        public async Task<Dictionary<string, decimal>> GetTotalExpensesBySanctuary()
        {
             var totalExpenses = await _dbContext.CostManagements
        .Include(cm => cm.Sanctuary) 
        .GroupBy(cm => cm.Sanctuary.Name)
        .Select(group => new
        {
            SanctuaryName = group.Key,
            TotalAmount = group.Sum(cm => cm.Amount)
        })
        .ToListAsync();
            return totalExpenses.ToDictionary(x => x.SanctuaryName, x => x.TotalAmount);
        }

        public async Task<List<CostManagement>> GetExpensesBySanctuary(string sanctuaryName)
        {
            var expenses = await _dbContext.CostManagements
                .Where(cm => cm.Sanctuary.Name.ToLower() == sanctuaryName.ToLower())  
                .Include(cm => cm.Sanctuary)  
                 
                .Select(cm => new CostManagement
                {
                    CostId = cm.CostId,
                    SanctuaryId=cm.SanctuaryId,
                    ExpenseType = cm.ExpenseType,
                    Amount = cm.Amount,
                    Date = cm.Date,
                    ResponsiblePersonId = cm.ResponsiblePersonId
                })
                .ToListAsync();

            return expenses;
        }


    }
}
