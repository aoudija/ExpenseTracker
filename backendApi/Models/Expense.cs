namespace backendApi.Models;

public enum ExpenseCategory
{
    Food,
    Transport,
    Entertainment,
    Utilities,
}

public class Expense
{
    public int Id { get; set; }
    
    public string? Name { get; set; }
    public ExpenseCategory Category { get; set; }
    
    public decimal Amount { get; set; }
    
    public int? UserId { get; set; }
    public User? User { get; set; } 
}
