using CV.MsAccount.AccessData.Interfaces;
using CV.MsAccount.AccessData.Persistence;
using CV.MsAccount.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CV.MsAccount.AccessData.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly AppDbContext _context;

        public CurrencyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Currency> GetCurrencyById(int currencyId)
        {
            var currency = await _context.Currencies.FirstOrDefaultAsync(c => c.CurrencyId == currencyId);
            return currency;
        }

        public async Task<Currency> GetCurrencyByName(string name)
        {
            var currency = await _context.Currencies.FirstOrDefaultAsync(c => c.Name == name);
            return currency;
        }
    }
}
