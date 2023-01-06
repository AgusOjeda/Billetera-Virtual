using Microsoft.EntityFrameworkCore;
using Transactions.AccessData.Interfaces;
using Transactions.Domain.Entities;

namespace Transactions.AccessData.Repositories
{
    public class MovementHistoryRepository : IMovementHistoryRepository
    {
        private readonly AppDbContext _context;

        public MovementHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddMovement(MovementHistory movementHistory)
        {
            _context.MovementsHistory.Add(movementHistory);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<MovementHistory>> GetMovementsByAccountId(Guid accountId)
        {
            return await _context.MovementsHistory.Where(
                s => s.FromAccountId == accountId || s.ToAccountId == accountId)
                .OrderByDescending(s => s.DateTimeTransaction)
                .ToListAsync();
        }

        public async Task<List<MovementHistory>> GetMovementsByDay(Guid accountId, DateTime date)
        {
            return await _context.MovementsHistory.Where(
                s =>
                s.DateTimeTransaction.Year == date.Year &&
                s.DateTimeTransaction.Month == date.Month &&
                s.DateTimeTransaction.Day == date.Day &&
                (s.FromAccountId == accountId || s.ToAccountId == accountId))
                .OrderByDescending(s => s.DateTimeTransaction)
                .ToListAsync();
        }
    }
}
