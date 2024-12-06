using backendApi.Models;

namespace backendApi.Services
{
    public interface IBudgetService
    {
        Task<Budget> SetBudget(Budget budget);
        Task<Budget?> GetBudgetStatus(int userId);
    }
}
