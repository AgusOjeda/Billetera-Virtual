using CV.MsAccount.Domain.Entities;

namespace CV.MsAccount.AccessData.Interface
{
    public interface IAccountRepository
    {
        Task CreateAccount(Account account);
        Task<List<Account>> GetAccounts();
        Task<Account> UpdateAccount(Account account);
        Task<Account> GetAccountById(Guid accountId);
        Task<Account> GetAccountByCbu(string cbu);
        Task<List<Account>> GetAccountByCustomerId(Guid accountId);
        Task Delete(Account account);
        Task<List<Account>> GetAccountsFromUser(Guid customerId);
        Task<Account> GetByCustomerId(Guid customerId);
        Task<Account> GetAccountByAlias(string alias);
    }
}