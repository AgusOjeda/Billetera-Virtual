
namespace CV.MsAccount.Application.Response
{
    public class AccountUserResponse
    {
        public string Alias { get; set; } = string.Empty;
        public string Cbu { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string Currency { get; set; } = null!;
        public string AccountState { get; set; } = null!;
    }
}
