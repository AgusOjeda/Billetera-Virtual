using Transactions.Domain.Entities;

namespace Transactions.AccessData.Interfaces
{
    public interface ITransactionStateRepository
    {
        Task<TransactionState> GetStateById(int id);
    }
}
