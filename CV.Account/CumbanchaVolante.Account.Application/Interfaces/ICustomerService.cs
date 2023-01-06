
using CV.MsAccount.Application.Response;

namespace CV.MsAccount.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerResponse> GetCustomerById(Guid customerId, string jwtToken);
    }
}
