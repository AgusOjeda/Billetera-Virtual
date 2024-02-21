using Microsoft.EntityFrameworkCore;
using CV.Onboarding.Domain.Entities;
using CV.Onboarding_AccessData.Interfaces;

namespace CV.Onboarding_AccessData.Repositories
{
    public  class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }
        
        //CRUD - CREATE - READ - UPDATE - DELETE
        
        public async Task CreateCustomer(Customer customer)
        {
            await _context.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _context.Customers
                            .Include(x=> x.Address)
                            .Include(x => x.IdentityVerification)
                            .ThenInclude(a => a.VerificationState)
                            .ToListAsync();
            
        }

        public void Update(Customer customer)
        {
            _context.Update(customer);
            _context.SaveChanges();
        }

        public void Delete(Customer customer)
        {
            _context.Remove(customer);
            _context.SaveChanges();
        }               

        public async Task<Customer?> GetCustomerById(Guid id)
        {
            return await _context.Customers.Where(x => x.Id == id)
                            .Include(x => x.Address)
                            .Include(x => x.IdentityVerification)
                            .ThenInclude(a => a.VerificationState)
                            .FirstOrDefaultAsync();
        }

        public async Task<Customer?> GetCustomerByDni(string dni)
        {
            return await _context.Customers.Where(x => x.Dni == dni)
                            .Include(x => x.Address)
                            .Include(x => x.IdentityVerification)
                            .ThenInclude(a => a.VerificationState)
                            .FirstOrDefaultAsync(); 
        }

        public async Task SetVerification(IdentityVerification customerVerificado)
        {
            await _context.AddAsync(customerVerificado);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer?> VerificationState(Guid id)
        {
            return await _context.Customers.Where(x => x.Id == id)
                            .Where(x => x.IdentityVerification.State == 1)
                            .Include(x => x.Address)
                            .Include(x => x.IdentityVerification)
                            .ThenInclude(a => a.VerificationState)
                            .FirstOrDefaultAsync();
        }
    }
}