using CV.Onboarding.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV.Onboarding_AccessData.Interfaces
{
    public interface IAddressRepository
    {
        Task CreateCustomerAddress(Address customer);
        Task<Address> CustomerAddressByID(Guid id);
        void Update(Address customer);
        void Delete(Address customer);
    }
}

