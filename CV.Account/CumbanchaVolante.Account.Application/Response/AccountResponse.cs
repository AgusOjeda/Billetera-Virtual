

namespace CV.MsAccount.Application.Response
{
    public class AccountResponse
    {
        public string Alias { get; set; } = string.Empty;
        public string Cbu { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string Cuil { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public string FullNameCustomer { get; set; } = string.Empty;
        public string AccountState { get; set; } = string.Empty;
    }
}
