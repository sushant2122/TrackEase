
using TrackEase.Models;

namespace TrackEase.Services.Interface;
public interface ITransactionService
{
    Task<bool> addTransaction(Transaction transaction);

    Task<List<Transaction>> GetAllTransactions();

    Task<bool> UpdateTransaction(Transaction updatedTransaction);


}
