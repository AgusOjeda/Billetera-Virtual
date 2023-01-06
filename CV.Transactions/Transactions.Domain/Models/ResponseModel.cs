namespace Transactions.Domain.Models
{
    public class ResponseModel
    {
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public AccountModel Data { get; set; }
    }
}
