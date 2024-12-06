using Microsoft.AspNetCore.Mvc;
using backendApi.Services;
using backendApi.Models;

[Route("api/[controller]")]
[ApiController]
public class BudgetController : ControllerBase
{
    private readonly IBudgetService _budgetService;

    public BudgetController(IBudgetService budgetService)
    {
        _budgetService = budgetService;
    }

    [HttpPost]
    public async Task<IActionResult> SetBudget([FromBody] Budget budget)
    {
        var updatedBudget = await _budgetService.SetBudget(budget);
        return Ok(updatedBudget);
    }

   [HttpGet("status/{userId}")]
    public async Task<IActionResult> GetBudgetStatus(int userId)
    {
        // Fetch the budget from the service
        var budget = await _budgetService.GetBudgetStatus(userId);

        // If no budget is found, return a 404 response
        if (budget == null)
            return NotFound("Budget not found for the user.");

        // Calculate the progress and return the details
        return Ok(new
        {
            budget.MonthlyLimit,
            budget.CurrentExpenses,
            Remaining = budget.MonthlyLimit - budget.CurrentExpenses,
            ProgressPercentage = budget.MonthlyLimit > 0
                ? (budget.CurrentExpenses / budget.MonthlyLimit) * 100
                : 0,
            Alert = budget.CurrentExpenses > budget.MonthlyLimit
                ? "You have exceeded your budget!"
                : "You are within your budget."
        });
    }


}
