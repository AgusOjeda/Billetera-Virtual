using System.ComponentModel.DataAnnotations;

namespace Transactions.Domain.DTOs.Request
{
    public class MovementsRequest
    {
        [Required]
        public Guid AccountId { get; set; }
    }
}
