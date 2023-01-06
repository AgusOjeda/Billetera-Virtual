using Transactions.Domain.Entities;

namespace Transactions.AccessData.Interfaces
{
    public interface IMovementHistoryRepository
    {
        Task<List<MovementHistory>> GetMovementsByAccountId(Guid accountId);
        Task<int> AddMovement(MovementHistory movementHistory);
        Task<List<MovementHistory>> GetMovementsByDay(Guid accountId, DateTime date);
    }
}
