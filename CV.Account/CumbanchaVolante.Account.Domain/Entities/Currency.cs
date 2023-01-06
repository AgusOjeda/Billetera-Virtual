namespace CV.MsAccount.Domain.Entities
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LongName { get; set; } = string.Empty;
        public ICollection<Account>? Accounts { get; set; }
    }
}