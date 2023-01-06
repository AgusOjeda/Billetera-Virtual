using CV.MsAccount.Application.Response;

namespace CV.MsAccount.Application.Interfaces
{
    public interface IAccountService
    {
        Task<AccountIdResponse> CreateAccount (Guid customerId, string shortNameCurrency);
        Task<AccountResponse> GetById(Guid accountId, string jwtToken);
        Task<AccountCbuResponse> GetByCbu(string cbu);
        Task<AccountBalanceResponse> UpdateAccountBalance(Guid accountId, decimal amount);
        Task<AccountStateResponse> UpdateAccountState(Guid accountId, string state);
        Task<List<AccountCustomerResponse>> GetByCustomerId(Guid customerId);
        Task<AccountIdResponse> DeleteAccount(Guid accountId);
        Task<List<AccountUserResponse>> GetAllFromCustomer(Guid customerId);
        Task<AccountIdResponse> UpdateAccountAlias(Guid accountId, string alias);
        Task<AccountIdResponse> UpdateAccountCurrency(Guid accountId, string shortNameCurrency);
        Task<AccountIdResponse> GetByAlias(string alias);
    }
}