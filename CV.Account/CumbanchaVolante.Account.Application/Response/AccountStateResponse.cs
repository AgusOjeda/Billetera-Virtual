
namespace CV.MsAccount.Application.Response
{
    public class AccountStateResponse
    {
        public Guid AccountId { get; set; }
        public Guid CustomerId { get; set; }
        public string AccountState { get; set; } = string.Empty;
    }
}
