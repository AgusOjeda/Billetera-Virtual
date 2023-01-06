
namespace CV.MsAccount.Domain.DTOs
{
    public class UpdateBalanceRequest
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
