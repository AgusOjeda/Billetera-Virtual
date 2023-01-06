using Microsoft.EntityFrameworkCore;
using Transactions.AccessData.Interfaces;
using Transactions.Domain.Entities;

namespace Transactions.AccessData.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly AppDbContext _context;

        public OperationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OperationType> GetOperationTypeById(int id)
        {
            return await _context.OperationTypes.FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
