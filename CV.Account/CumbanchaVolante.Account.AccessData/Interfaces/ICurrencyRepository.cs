
using CV.MsAccount.Domain.Entities;

namespace CV.MsAccount.AccessData.Interfaces
{
    public interface ICurrencyRepository
    {
        Task<Currency> GetCurrencyByName(string name);
        Task<Currency> GetCurrencyById(int currencyId);
    }
}
