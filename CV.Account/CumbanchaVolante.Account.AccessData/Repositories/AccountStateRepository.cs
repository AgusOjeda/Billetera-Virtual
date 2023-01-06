

using CV.MsAccount.AccessData.Interfaces;
using CV.MsAccount.AccessData.Persistence;
using CV.MsAccount.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CV.MsAccount.AccessData.Repositories
{
    public class AccountStateRepository : IAccountStateRepository
    {
        private readonly AppDbContext _context;

        public AccountStateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AccountState> GetAccountStateByName(string name)
        {
            var account= await _context.AccountStates.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
            return account;
        }
    }
}
