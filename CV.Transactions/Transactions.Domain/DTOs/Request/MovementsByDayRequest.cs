using System.ComponentModel.DataAnnotations;

namespace Transactions.Domain.DTOs.Request
{
    public class MovementsByDayRequest
    {
        [Required]
        public Guid AccountId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
    }
}
