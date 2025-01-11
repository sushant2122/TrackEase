using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackEase.Abstraction;
using TrackEase.Models;
using TrackEase.Services.Interface;
using TrackEase.Util;


public class DebtService : BaseService<Debt>, IDebtService
{
    private List<Debt> _debts = new List<Debt>(); 
    private static readonly string AppDebtsFilePath = Util.GetAppDebtsFilePath();

    // Simulated retrieval logic
    public DebtService()
    {
        _debts = GetAll(AppDebtsFilePath);

    }
    public List<Debt> GetAllDebts()
    {
        return _debts;
    }

    public async Task<bool> addDebt(Debt debt)
    {
        try
        {
            // Validate debt properties to avoid invalid data
            if (string.IsNullOrEmpty(debt.Title) || debt.Amount <= 0)
            {
                throw new ArgumentException("Debt title and amount must be provided.");
            }

            // Create a new Debt object with a unique Id and add it to the list
            var newDebt = new Debt
            {
                Id = _debts.Any() ? _debts.Max(t => t.Id) + 1 : 1, // Assign a unique Id
                Title = debt.Title,
                Source = debt.Source,
                Amount = debt.Amount,
                DueDate = debt.DueDate,
                ClearedDate = debt.ClearedDate,
                Status = debt.Status
            };

            _debts.Add(newDebt);

            // Save the updated list of debts to the file
            SaveAll(_debts, AppDebtsFilePath); // Ensure SaveAll handles potential issues
            return true; // Return true to indicate success
        }
        catch (Exception ex)
        {
            // Log the error and return false to indicate failure
            Console.WriteLine($"Error adding debt: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> ClearDebt(int debtId)
    {
        try
        {
            // Find the debt by its ID
            var debt = _debts.FirstOrDefault(d => d.Id == debtId);

            if (debt == null)
            {
                // Debt not found
                Console.WriteLine($"Debt with ID {debtId} not found.");
                return false;
            }

            // Update the debt status to 'Cleared' and set the ClearedDate
            debt.Status = "Cleared";
            debt.ClearedDate = DateOnly.FromDateTime(DateTime.Now); // Set the cleared date to current date

            // Save the updated list of debts to the file
            SaveAll(_debts, AppDebtsFilePath); // Ensure SaveAll handles potential issues

            Console.WriteLine($"Debt with ID {debtId} has been cleared.");
            return true; // Return true to indicate success
        }
        catch (Exception ex)
        {
            // Log the error and return false to indicate failure
            Console.WriteLine($"Error clearing debt: {ex.Message}");
            return false;
        }
    }

    
}