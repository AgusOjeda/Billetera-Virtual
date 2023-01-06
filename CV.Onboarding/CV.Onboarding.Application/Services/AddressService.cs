using CV.Onboarding.Application.Interfaces;
using CV.Onboarding.Domain.DTOs;
using CV.Onboarding.Domain.Entities;
using CV.Onboarding_AccessData.Interfaces;

namespace CV.Onboarding.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repository;
        private readonly IEntityMapperCustomer _mapper;
        public AddressService(IAddressRepository repository, IEntityMapperCustomer mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> CreateCustomerAddress(AddressRequest customer, Guid Id)
        {

            var mappedCustomer = _mapper.AddressForDisplayToAddress(customer, Id);
            
            await _repository.CreateCustomerAddress(mappedCustomer);
            if (await _repository.CustomerAddressByID(Id) != null)
            {
                return true;
            }
            else { return false; }
        }
    }
}
