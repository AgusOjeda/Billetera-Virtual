
namespace CV.MsAccount.Application.Response
{
    public class AccountBalanceResponse
    {
        public Guid AccountId { get; set; }
        public decimal Balance { get; set; }
        public string NameCurrency { get; set; } = string.Empty;
    }
}
