using CV.MsAccount.AccessData.Interfaces;
using CV.MsAccount.Application.Interfaces;
using CV.MsAccount.Application.Response;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CV.MsAccount.Application.Services
{
    public  class CustomerService: ICustomerService
    {
        private readonly IConfiguration _configuration;

        public CustomerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<CustomerResponse> GetCustomerById(Guid customerId, string jwtToken)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(_configuration["ApiSettings:baseUrl"])
            };

            var url = $"api/Customer";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(jwtToken);

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var customerResult = JsonConvert.DeserializeObject<ServerResponse>(result);
                var customer = JsonConvert.SerializeObject(customerResult.Data);                
                var customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(customer);
                return customerResponse;
            };
            return null;
        }

    }
}
