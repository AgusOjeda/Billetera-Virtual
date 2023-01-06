using CV.Onboarding.Domain.Entities;
using CV.Onboarding_AccessData.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV.Onboarding_AccessData.Repositories
{
    
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _context;

        public AddressRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateCustomerAddress(Address customerAddress)
        {
            await _context.AddAsync(customerAddress);
            await _context.SaveChangesAsync();
        }

        public void Delete(Address customer)
        {
            throw new NotImplementedException();
        }

        public void Update(Address customer)
        {
            throw new NotImplementedException();
        }

        public async Task<Address> CustomerAddressByID(Guid id)
        {
            return await _context.Address.Where(x=>x.CustomerId == id).FirstOrDefaultAsync();
            ;
        }

    }
}
