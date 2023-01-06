using Transactions.Domain.Entities;

namespace Transactions.AccessData.Interfaces
{
    public interface IOperationRepository
    {
        Task<OperationType> GetOperationTypeById(int id);
    }
}
