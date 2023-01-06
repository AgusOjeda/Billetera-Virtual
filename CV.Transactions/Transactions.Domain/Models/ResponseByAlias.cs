namespace Transactions.Domain.Models
{
    public class ResponseByAlias
    {
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public AccountByAlias Data { get; set; }
    }
}
