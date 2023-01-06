using Microsoft.EntityFrameworkCore;
using Transactions.AccessData.Interfaces;
using Transactions.Domain.Entities;

namespace Transactions.AccessData.Repositories
{
    public class TransactionStateRepository : ITransactionStateRepository
    {
        private readonly AppDbContext _context;

        public TransactionStateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TransactionState> GetStateById(int id)
        {
            return await _context.TransactionStates.FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
