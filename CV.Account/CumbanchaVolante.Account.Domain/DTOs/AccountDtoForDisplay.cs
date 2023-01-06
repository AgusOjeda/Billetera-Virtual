namespace CV.MsAccount.Domain.DTOs
{
    public class AccountDtoForDisplay
    {
        //public Guid Id { get; set; }
        //public Guid CustomerId { get; set; }
        public string Alias { get; set; } = null!;
        public string Cbu { get; set; } = null!;
        public double Balance { get; set; }

        public  CurrencyDtoForDisplay Currency { get; set; } = null!;
        public  AccountStateDtoForDisplay? StateNavigation { get; set; }
    }
}