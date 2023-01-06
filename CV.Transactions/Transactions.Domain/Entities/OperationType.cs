namespace Transactions.Domain.Entities
{
    public class OperationType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
