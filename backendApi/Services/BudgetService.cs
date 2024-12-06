using Microsoft.EntityFrameworkCore;
using backendApi.Data;
using backendApi.Models;

namespace backendApi.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly AppDbContext _context;

        public BudgetService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Budget> SetBudget(Budget budget)
        {
            var userBudget = await _context.Budgets.FirstOrDefaultAsync(b => b.UserId == budget.UserId);
            if (userBudget != null)
            {
                userBudget.MonthlyLimit = budget.MonthlyLimit;
            }
            else
            {
                _context.Budgets.Add(budget);
            }

            await _context.SaveChangesAsync();
            return budget;
        }

        public async Task<Budget?> GetBudgetStatus(int userId)
        {
            var budget = await _context.Budgets.FirstOrDefaultAsync(b => b.UserId == userId);
            if (budget == null) return null;

            return budget; // Return the actual Budget object
        }


    }
}
