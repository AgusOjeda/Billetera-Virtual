
namespace CV.MsAccount.Application.Response
{
    public class AccountCbuResponse
    {
        public Guid AccountId { get; set; }
        public string Alias { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string NameCurrency { get; set; } = string.Empty;
        public string Cbu { get; set; } = string.Empty;
        public string LongNameCurrency { get; set; } = string.Empty;
    }
}
