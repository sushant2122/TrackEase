

namespace TrackEase.Models;

public class Transaction
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string TagName { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } // Income, Expense, Debt
    public string Notes { get; set; }
}

