namespace Transactions.Domain.Models
{
    public class UpdateBalanceModel
    {
        public Guid AccountId { get; set; }
        public Decimal Amount { get; set; }
    }
}
