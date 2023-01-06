namespace Transactions.Domain.Models
{
    public class ResponseByCbu
    {
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public AccountByCbu Data { get; set; }
    }
}
