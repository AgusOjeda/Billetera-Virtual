namespace Transactions.Domain.Models
{
    public class AccountModel
    {
        public string FullNameCustomer { get; set; }
        public decimal Balance { get; set; }
        public string Alias { get; set; }
        public string Cbu { get; set; }
        public string Dni { get; set; }
        public string Cuil { get; set; }
        public string Currency { get; set; }
        public string AccountState { get; set; }
    }
}
