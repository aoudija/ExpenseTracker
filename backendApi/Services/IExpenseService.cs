using backendApi.Models;

namespace backendApi.Services
{
    public interface IExpenseService
    {
        Task AddExpense(Expense expense);
        Task<IEnumerable<Expense>> GetExpensesByUser(int userId);
        Task<Expense?> GetExpenseById(int id);
        Task<bool> DeleteExpense(int id);
    }
}
