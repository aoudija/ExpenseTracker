using Microsoft.AspNetCore.Mvc;
using backendApi.Models;
using backendApi.Services;

namespace backendApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    // To add a new expense
    [HttpPost]
    public async Task<IActionResult> AddExpense([FromBody] Expense expense)
    {
        if (expense == null)
            return BadRequest("Expense data is required.");

        if (!Enum.IsDefined(typeof(ExpenseCategory), expense.Category))
            return BadRequest("Invalid category. Allowed values are: Food, Transport, Entertainment, Utilities.");

        await _expenseService.AddExpense(expense);
        return CreatedAtAction(nameof(GetExpenseById), new { id = expense.Id }, expense);
    }

    // View all expenses for a specific user
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetExpensesByUser(int userId)
    {
        var expenses = await _expenseService.GetExpensesByUser(userId);
        if (expenses == null || !expenses.Any())
            return NotFound("No expenses found for the user.");

        return Ok(expenses);
    }

    // Delete an expense by ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        var deleted = await _expenseService.DeleteExpense(id);
        if (!deleted)
            return NotFound("Expense not found.");

        return NoContent();
    }

    // Get an expense by ID (Helper endpoint)
    [HttpGet("{id}")]
    public async Task<IActionResult> GetExpenseById(int id)
    {
        var expense = await _expenseService.GetExpenseById(id);
        if (expense == null)
            return NotFound();

        return Ok(expense);
    }
}
