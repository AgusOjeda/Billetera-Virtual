using Transactions.Domain.Entities;

namespace Transactions.AccessData.Interfaces
{
    public interface ITransactionRepository
    {
        Task<int> AddTransaction(Transaction transaction);
    }
}
