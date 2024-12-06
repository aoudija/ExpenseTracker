namespace backendApi.Models;

public class User
{
    public int Id { get; set; }
    
    public string? Username { get; set; }
    public string? Email { get; set; } 
    
    public ICollection<Expense>? Expenses { get; set; } 
    
    // Nullable Budget (reference type, so it can be null)
    public Budget? Budget { get; set; }
}