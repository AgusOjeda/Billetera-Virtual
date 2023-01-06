using System.ComponentModel.DataAnnotations;

namespace Transactions.Domain.DTOs.Request
{
    public class TransactionsRequest
    {
        [Required]
        public Guid FromAccountId { get; set; }
        public Guid ToAccountId { get; set; }
        //public bool DestinationIsByCbu { get; set; }
        public int OperationType { get; set; }
        public string Reason { get; set; }
        public float Amount { get; set; }
    }
}
