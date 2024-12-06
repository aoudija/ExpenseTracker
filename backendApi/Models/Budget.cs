namespace backendApi.Models;

public class Budget
{
    public int Id { get; set; }
    public decimal MonthlyLimit { get; set; }
    public decimal CurrentExpenses { get; set; }
    public int? UserId { get; set; }
    public User? User { get; set; }
}
