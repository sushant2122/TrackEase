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

            // Add the transaction to the list
            _transactions.Add(new Transaction
            {
                Title = transaction.Title,
                Description = transaction.Description,
                TagName = transaction.TagName,
                Date = transaction.Date,
                Amount = transaction.Amount,
                Type = transaction.Type,
                Notes = transaction.Notes
            });

            // Save the transactions
            SaveAll(_transactions, AppTransactionsFilePath);
            return true; // Return true to indicate success
        }
        catch (Exception ex)
        {
            // Log the error (you can replace this with a UI message or other appropriate handling)
            Console.WriteLine($"Error: {ex.Message}");
            return false; // Return false to indicate failure
        }
    }
    


    // Simulated SearchUser logic
    public async Task<List<Transaction>> SearchTransactions(string searchTitle)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(searchTitle))
                throw new ArgumentNullException("Title Cannot be Null or Empty", nameof(searchTitle));

            return await Task.FromResult(
                _transactions.Where(t => t.Title.Contains(searchTitle, StringComparison.OrdinalIgnoreCase)).ToList());

        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while searching for the user.", ex);
        }
    }
}