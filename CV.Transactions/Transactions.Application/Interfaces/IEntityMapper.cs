using Transactions.Domain.DTOs;
using Transactions.Domain.Entities;

namespace Transactions.Application.Interfaces
{
    public interface IEntityMapper
    {
        MovementHistoryDto ToMovementHistoryDto(MovementHistory movementHistoryEntity);
        TransactionDto ToTransactionDto(Transaction transactionEntity);
    }
}