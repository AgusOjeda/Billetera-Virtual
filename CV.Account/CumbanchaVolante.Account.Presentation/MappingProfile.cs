using AutoMapper;
using CV.MsAccount.Domain.Entities;
using CV.MsAccount.Domain.DTOs;

namespace CV.MsAccount.Presentation
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Account,AccountDtoForDisplay>();
            CreateMap<AccountState, AccountStateDtoForDisplay>();
            CreateMap<Currency, CurrencyDtoForDisplay>();
        }
    }
}