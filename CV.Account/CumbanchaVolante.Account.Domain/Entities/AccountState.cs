namespace CV.MsAccount.Domain.Entities
{
    public class AccountState
    {
        public int AccountStateId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Account>? Accounts { get; set; }
    }
}