namespace Transactions.Domain.DTOs
{
    public class MovementHistoryDto
    {
        public string OperationType { get; set; }
        public Guid FromAccountId { get; set; }
        public string FromCbu { get; set; }
        public string FullNameEmisorCustomer { get; set; }
        public string DniEmisorCustomer { get; set; }
        public string CuilEmisorCustomer { get; set; }
        public Guid ToAccountId { get; set; }
        public string ToCbu { get; set; }
        public string FullNameReceiverCustomer { get; set; }
        public string DniReceiverCustomer { get; set; }
        public string CuilReceiverCustomer { get; set; }
        public DateTime DateTimeTransaction { get; set; }
        public Decimal AmountTransaction { get; set; }
        public string Currency { get; set; }
        public string ResultingStateOfTransaction { get; set; }
    }
}
