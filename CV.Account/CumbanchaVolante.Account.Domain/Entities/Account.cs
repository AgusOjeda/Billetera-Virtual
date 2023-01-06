
namespace CV.MsAccount.Domain.Entities
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public string Alias { get; set; } = string.Empty;
        public string Cbu { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public Guid CustomerId { get; set; }
        public Currency Currency { get; set; } = null!;
        public int CurrencyId { get; set; }
        public AccountState? AccountState { get; set; }
        public int? AccountStateId { get; set; }
    }
}