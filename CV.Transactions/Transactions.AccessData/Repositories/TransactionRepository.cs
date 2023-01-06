using Transactions.AccessData.Interfaces;
using Transactions.Domain.Entities;

namespace Transactions.AccessData.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            return await _context.SaveChangesAsync();
        }

    }
}
