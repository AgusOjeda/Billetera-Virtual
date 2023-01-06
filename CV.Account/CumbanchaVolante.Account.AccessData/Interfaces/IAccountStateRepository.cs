using CV.MsAccount.Domain.Entities;

namespace CV.MsAccount.AccessData.Interfaces
{
    public interface IAccountStateRepository
    {
        Task<AccountState> GetAccountStateByName(string name);

    }
}
