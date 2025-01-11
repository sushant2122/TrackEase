using System;

namespace TrackEase.Models;

public class Debt
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Source { get; set; }

    public decimal Amount { get; set; } 

    public DateOnly DueDate { get; set; } = DateOnly.FromDateTime(DateTime.Now.AddDays(30));

    public DateOnly? ClearedDate { get; set; }
    
    public  string Status { get; set; } = "Pending";
}