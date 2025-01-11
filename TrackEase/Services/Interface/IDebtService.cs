using TrackEase.Models;

namespace TrackEase.Services.Interface;

public interface IDebtService
{
    Task<bool> addDebt(Debt debt);

    Task<List<Debt>> GetAllDebts();
}