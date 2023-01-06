using CV.MsAccount.AccessData.Interface;
using CV.MsAccount.AccessData.Persistence;
using CV.MsAccount.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CV.MsAccount.AccessData.Repositories
{
    public  class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAccount(Account account) 
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Account account)
        {
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
        }

        public async Task<Account> GetAccountByAlias(string alias)
        {
            var accounts = await GetAccounts();
            var account = accounts.FirstOrDefault(a => a.Alias == alias);
            return account;
        }

        public async Task<Account> GetAccountByCbu(string cbu)
        {
            var accounts = await GetAccounts();
            var account = accounts.FirstOrDefault(a => a.Cbu == cbu);
            return account;
        }

        public async Task<List<Account>> GetAccountByCustomerId(Guid customerId)
        {
            var accounts = await GetAccounts();
            var account = accounts.Where(a => a.CustomerId == customerId).ToList();
            return account;
        }

        public async Task<Account> GetAccountById(Guid accountId)
        {
            var accounts = await GetAccounts();
            var account = accounts.FirstOrDefault(a => a.AccountId == accountId);
            return account;
        }

        public async Task<List<Account>> GetAccounts()
        {
            var accounts = await _context.Accounts.Include(a => a.Currency).Include(a => a.AccountState).ToListAsync();
            return accounts;
        }

        public async Task<List<Account>> GetAccountsFromUser(Guid customerId)
        {
            var accounts = await _context.Accounts.Include(a => a.Currency).Include(a => a.AccountState).Where(a => a.CustomerId == customerId).ToListAsync();
            return accounts;
        }

        public async Task<Account> GetByCustomerId(Guid customerId)
        {
            var accounts = await GetAccounts();
            var account = accounts.FirstOrDefault(a => a.CustomerId == customerId);
            return account;
        }

        public async Task<Account> UpdateAccount(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return account;
        }
    }
}