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

    public async Task<List<Transaction>> GetAllTransactions()
    {
        return _transactions;
    }

    public async Task<bool> UpdateTransaction(Transaction updatedTransaction)
    {
        try
        {
            var transaction = _transactions.FirstOrDefault(t => t.Id == updatedTransaction.Id);

            if (transaction == null)
            {
                return false;
            }

            transaction.Title = updatedTransaction.Title;
            transaction.TagName = updatedTransaction.TagName;
            transaction.Date = updatedTransaction.Date;
            transaction.Amount = updatedTransaction.Amount;
            transaction.Type = updatedTransaction.Type;
            transaction.Notes = updatedTransaction.Notes;

            SaveAll(_transactions, AppTransactionsFilePath);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    

    public async Task<bool> addTransaction(Transaction transaction)
    {
        try
        {
            var newTransaction = new Transaction
            {
                Id = _transactions.Any() ? _transactions.Max(t => t.Id) + 1 : 1, // Assign a unique Id
                Title = transaction.Title,
                TagName = transaction.TagName,
                Date = transaction.Date,
                Amount = transaction.Amount,
                Type = transaction.Type,
                Notes = transaction.Notes
            };

            _transactions.Add(newTransaction);

            SaveAll(_transactions, AppTransactionsFilePath);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
   

}
