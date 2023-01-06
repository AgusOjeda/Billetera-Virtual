using CV.Onboarding_AccessData.Interfaces;
using CV.Onboarding.Domain.DTOs;
using CV.Onboarding.Domain.Entities;
using CV.Onboarding.Application.Interfaces;

namespace CV.Onboarding.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IEntityMapperCustomer _mapper;

        public CustomerService(ICustomerRepository repository, IEntityMapperCustomer mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<CustomerDtoForDisplay>> GetAllCustomers()
        {
            var customersList = await _repository.GetAllCustomers();
            var mappedCustomersList = new List<CustomerDtoForDisplay>();

            foreach (var customer in customersList)
                mappedCustomersList.Add(_mapper.CustomerToCustomerDtoForDisplay(customer));

            return mappedCustomersList;
        }
        public async Task<CustomerDtoForDisplay> GetCustomerById(Guid id)
        {
            var customer = await _repository.GetCustomerById(id);
           

            if (customer != null)
            {
                return _mapper.CustomerToCustomerDtoForDisplay(customer);
            }

            return null;
        }
        public async Task<CustomerDtoForDisplay> GetCustomerByDni(string dni)
        {
            var customer = await _repository.GetCustomerByDni(dni);

            if (customer != null)
            {                
                return _mapper.CustomerToCustomerDtoForDisplay(customer);
            }
            else
            {
                return null;
            }
            
        }
        public async Task<Customer> CreateCustomer(CustomerRequest customerviene, string Id)
        {
            Guid.TryParse(Id, out Guid userId);
            var customer = new Customer
            {
                Id = userId,
                FirstName = customerviene.FirstName,
                LastName = customerviene.LastName,
                Dni = customerviene.Dni,
                Cuil = customerviene.Cuil,
                Phone = customerviene.Phone                
            };

            await _repository.CreateCustomer(customer);
            var customerCreate = await _repository.GetCustomerByDni(customerviene.Dni);
            if (customerCreate != null)
            {
                var verification = new IdentityVerification
                {
                    State = 2,
                    CustomerId = customerCreate.Id,
                    IdentityInformation = "No verificado"
                };
                await _repository.SetVerification(verification);
                return customerCreate;
            }
            else { return null; }
        }
        public async Task<bool> VerificationSet(Guid id)
        {
            var verification = new IdentityVerification
            {
                State = 1,
                CustomerId =id,
                IdentityInformation = "Verificado"                           
            };
            await _repository.SetVerification(verification);
            if (await _repository.VerificationState(id) != null){return true;}
            else { return false; }
        }

        public async Task<CustomerDtoForDisplay> VerificationState(Guid id)
        {
            var customer = await _repository.VerificationState(id);
            if (customer != null)
            {
                return  _mapper.CustomerToCustomerDtoForDisplay(customer);
            }
            else { return null; }

        }
        public async Task<bool> ValidateDni(string dni)
        {
            var customer = await _repository.GetCustomerByDni(dni);

            if (customer == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
