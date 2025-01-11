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

    public async Task<bool> UpdateTransaction(Transaction updatedTransaction)
    {
        try
        {
            // Find the transaction by ID
            var transaction = _transactions.FirstOrDefault(t => t.Id == updatedTransaction.Id);

            if (transaction == null)
            {
                // Transaction not found
                Console.WriteLine($"Transaction with ID {updatedTransaction.Id} not found.");
                return false;
            }

            // Update the transaction fields
            transaction.Title = updatedTransaction.Title;
            transaction.Description = updatedTransaction.Description;
            transaction.TagName = updatedTransaction.TagName;
            transaction.Date = updatedTransaction.Date;
            transaction.Amount = updatedTransaction.Amount;
            transaction.Type = updatedTransaction.Type;
            transaction.Notes = updatedTransaction.Notes;

            // Simulate saving to a database or file system (you can replace this with actual save logic)
           SaveAll(_transactions, AppTransactionsFilePath);

            Console.WriteLine($"Transaction with ID {updatedTransaction.Id} updated successfully.");
            return true;
        }
        catch (Exception ex)
        {
            // Log the error and return false to indicate failure
            Console.WriteLine($"Error updating transaction: {ex.Message}");
            return false;
        }
    }
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