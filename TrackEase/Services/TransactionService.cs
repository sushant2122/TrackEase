using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackEase.Abstraction;
using TrackEase.Models;
using TrackEase.Services.Interface;
using TrackEase.Util;


public class TransactionService : BaseService<Transaction>, ITransactionService
{
    private List<Transaction> _transactions;
    private static readonly string AppTransactionsFilePath = Util.GetAppTransactionsFilePath();
    public TransactionService()
    {
        _transactions = GetAll(AppTransactionsFilePath);
        


      
    }

    // Simulated retrieval logic
    public async Task<List<Transaction>> GetAllTransactions()
    {
     
        return _transactions;
        
    }

    // Simulated Register New Users logic
    public async Task<bool> addTransaction(Transaction transaction)
    {
        try
        {
            // Create a new Transaction object with a unique Id and add it to the list
            var newTransaction = new Transaction
            {
                Id = _transactions.Any() ? _transactions.Max(t => t.Id) + 1 : 1, // Assign a unique Id
                Title = transaction.Title,
                Description = transaction.Description,
                TagName = transaction.TagName,
                Date = transaction.Date,
                Amount = transaction.Amount,
                Type = transaction.Type,
                Notes = transaction.Notes
            };

            _transactions.Add(newTransaction);

            // Save the updated list of transactions to the file
            SaveAll(_transactions, AppTransactionsFilePath);
            return true; // Return true to indicate success
        }
        catch (Exception ex)
        {
            // Log the error and return false to indicate failure
            Console.WriteLine($"Error adding transaction: {ex.Message}");
            return false;
        }
    }

    
}