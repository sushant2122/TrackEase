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
    private List<Debt> _debts;
    private static readonly string AppDebtsFilePath = Util.GetAppDebtsFilePath();
    public DebtService()
    {
        _debts = GetAll(AppDebtsFilePath);
        
    }

    // Simulated retrieval logic
    public async Task<List<Debt>> GetAllDebts()
    {
     
        return _debts;
        
    }

    // Simulated Register New Users logic
    public async Task<bool> addDebt(Debt debt)
    {
        try
        {
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
            SaveAll(_debts, AppDebtsFilePath);
            return true; // Return true to indicate success
        }
        catch (Exception ex)
        {
            // Log the error and return false to indicate failure
            Console.WriteLine($"Error adding debt: {ex.Message}");
            return false;
        }
    }

    
}