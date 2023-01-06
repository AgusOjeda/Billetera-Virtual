using Transactions.Domain.DTOs;
using Transactions.Domain.Entities;
using Transactions.Domain.Models;

namespace Transactions.Application.Interfaces
{
    public interface IMovementHistoryService
    {
        Task<List<MovementHistoryDto>> GetMovementsByAccountId(Guid accountId);
        Task<List<MovementHistoryDto>> GetMovementsByDay(Guid accountId, DateTime date);
        Task<int> AddMovement(MovementHistory movementHistory);
        Task<bool> AddMovementFromTransaction(Transaction transaction, AccountModel emisorAccountData, AccountModel receiverAccountData);
        Task<bool> AddMovementRejected(decimal amount, int operationTypeId, Guid fromAccountId, Guid toAccountId, AccountModel emisorAccountData, AccountModel receiverAccountData);
    }
}
