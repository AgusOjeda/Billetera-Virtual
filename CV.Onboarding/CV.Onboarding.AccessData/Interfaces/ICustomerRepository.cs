using CV.Onboarding.Domain.Entities;


namespace CV.Onboarding_AccessData.Interfaces
{
    public interface ICustomerRepository
    {
        Task CreateCustomer(Customer customer);
        Task<List<Customer>> GetAllCustomers();
        void Update(Customer customer);
        void Delete(Customer customer);
        Task<Customer> GetCustomerById(Guid id);
        Task<Customer> GetCustomerByDni(string dni);
        Task SetVerification(IdentityVerification customerVerificado);
        Task<Customer> VerificationState(Guid id);

    }
}
