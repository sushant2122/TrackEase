using TrackEase.Models;

namespace TrackEase.Services.Interface;

public interface IDebtService
{
    Task<bool> addDebt(Debt debt);

    List<Debt> GetAllDebts();
}