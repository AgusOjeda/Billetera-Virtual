
namespace CV.MsAccount.Domain.Models
{
    public class AccountRequest
    {
        public string Alias { get; set; } = string.Empty;
        public string Cbu { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public Guid CustomerId { get; set; }
        public int CurrencyId { get; set; }
        public int? AccountStateId { get; set; }
    }
}
