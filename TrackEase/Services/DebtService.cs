using TrackEase.Abstraction;
using TrackEase.Models;
using TrackEase.Services.Interface;
using TrackEase.Util;

public class DebtService : BaseService<Debt>, IDebtService
{
    private List<Debt> _debts = new List<Debt>();
    private static readonly string AppDebtsFilePath = Util.GetAppDebtsFilePath();

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
            if (string.IsNullOrEmpty(debt.Title) || debt.Amount <= 0)
            {
                throw new ArgumentException("Debt title and amount must be provided.");
            }

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

            SaveAll(_debts, AppDebtsFilePath);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> ClearDebt(int debtId)
    {
        try
        {
            var debt = _debts.FirstOrDefault(d => d.Id == debtId);

            if (debt == null)
            {
                return false;
            }

            debt.Status = "Cleared";
            debt.ClearedDate = DateOnly.FromDateTime(DateTime.Now);

            SaveAll(_debts, AppDebtsFilePath);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}