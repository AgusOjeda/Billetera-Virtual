using CV.Onboarding.Application.Interfaces;
using CV.Onboarding.Domain.DTOs;
using CV.Onboarding.Domain.Entities;

namespace CV.Onboarding.Application.Mapper
{
    public class EntityMapperCustomer : IEntityMapperCustomer
    {
        public Address AddressForDisplayToAddress(AddressRequest addressEntity, Guid id)
        {
            var customer = new Address()
            {
                CustomerId = id,
                Street = addressEntity.Street,
                Number = addressEntity.Number,
                Location = addressEntity.Location,
                Province = addressEntity.Province,
            };
            return customer;
        }

        public CustomerDtoForDisplay CustomerToCustomerDtoForDisplay(Customer customerEntity)
        {
            var customer = new CustomerDtoForDisplay()
            {
                FirstName = customerEntity.FirstName,
                LastName = customerEntity.LastName,
                Dni = customerEntity.Dni,
                Cuil = customerEntity.Cuil,
                Phone = customerEntity.Phone,
                VerificationState = customerEntity.IdentityVerification.IdentityInformation,
                Address = new AddressDTO {
                    Location= customerEntity.Address.Location,
                    Number= customerEntity.Address.Number,
                    Province= customerEntity.Address.Province,
                    Street= customerEntity.Address.Street,
                }
            };
            return customer;
        }
    }
}