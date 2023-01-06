using CV.Onboarding.Domain.DTOs;
using CV.Onboarding.Domain.Entities;

namespace CV.Onboarding.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerDtoForDisplay>> GetAllCustomers();
        Task<CustomerDtoForDisplay> GetCustomerById(Guid id);
        Task<Customer> CreateCustomer(CustomerRequest customer, string Id);
        Task<CustomerDtoForDisplay> GetCustomerByDni(string dni);
        Task<bool> VerificationSet(Guid id);
        Task<CustomerDtoForDisplay> VerificationState(Guid id);
        Task<bool> ValidateDni(string dni);
    }
}