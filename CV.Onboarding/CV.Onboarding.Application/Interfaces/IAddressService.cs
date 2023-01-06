using CV.Onboarding.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV.Onboarding.Application.Interfaces
{
    public interface IAddressService
    {
        Task<bool> CreateCustomerAddress(AddressRequest customer, Guid Id);
    }
}
