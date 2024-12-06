using Microsoft.EntityFrameworkCore;
using backendApi.Data;
using backendApi.Models;
using backendApi.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace backendApi.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;


        public ExpenseService(AppDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }


        public async Task AddExpense(Expense expense)
        {
            _context.Expenses.Add(expense);

            var budget = await _context.Budgets.FirstOrDefaultAsync(b => b.UserId == expense.UserId);
            if (budget != null)
            {
                budget.CurrentExpenses += expense.Amount;
                if (budget.CurrentExpenses > budget.MonthlyLimit)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("White on blue.");
                    Console.WriteLine("Another line.");
                    Console.ResetColor();
                    await _hubContext.Clients.User(expense.UserId.ToString())
                        .SendAsync("ReceiveNotification", $"You have exceeded your budget! Limit: {budget.MonthlyLimit}, Expenses: {budget.CurrentExpenses}");
                }
                _context.Budgets.Update(budget);
            }

            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Expense>> GetExpensesByUser(int userId)
        {
            return await _context.Expenses
                .Where(e => e.UserId == userId)
                .Include(e => e.User)
                .ToListAsync();
        }

        public async Task<Expense?> GetExpenseById(int id)
        {
            return await _context.Expenses
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
                return false;

            var budget = await _context.Budgets.FirstOrDefaultAsync(b => b.UserId == expense.UserId);
            if (budget != null)
            {
                budget.CurrentExpenses -= expense.Amount;
                _context.Budgets.Update(budget);
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
