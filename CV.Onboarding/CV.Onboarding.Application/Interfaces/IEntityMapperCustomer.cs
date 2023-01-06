using CV.Onboarding.Domain.DTOs;
using CV.Onboarding.Domain.Entities;

namespace CV.Onboarding.Application.Interfaces
{
    public interface IEntityMapperCustomer
    {
        CustomerDtoForDisplay CustomerToCustomerDtoForDisplay(Customer customerEntity);
        Address AddressForDisplayToAddress(AddressRequest addressEntity, Guid id);
    }
}