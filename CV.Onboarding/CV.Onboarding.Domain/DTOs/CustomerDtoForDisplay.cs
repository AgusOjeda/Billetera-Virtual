using CV.Onboarding.Domain.Entities;

namespace CV.Onboarding.Domain.DTOs
{
    public class CustomerDtoForDisplay
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Dni { get; set; }
        public string? Cuil { get; set; }
        public string? Phone { get; set; }
        public AddressDTO? Address { get; set; }
        public string? VerificationState { get; set; } 
    }
}