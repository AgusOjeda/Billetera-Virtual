namespace Transactions.Domain.DTOs
{
    public class TransactionDto
    {
        public Guid FromAccountId { get; set; }
        public Guid ToAccountId { get; set; }
        public int State { get; set; }
        public int OperationTypeId { get; set; }
        public string Reason { get; set; }
        public Decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
    }
}
