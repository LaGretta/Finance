namespace FinanceAPI.Models;

public class Expense
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime CreateAt { get; set; } =  DateTime.UtcNow;

    public User User { get; set; } = null;
}