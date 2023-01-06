using CV.Onboarding.Application.Interfaces;
using CV.Onboarding.Domain.DTOs;
using CV.Onboarding.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;

namespace CV.Onboarding.Presentation.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _serviceCustomer;
        private readonly IAddressService _serviceAddress;
        private readonly HttpClient _httpClient;

        public CustomerController(ICustomerService service, IAddressService serviceAddress)
        {
            _serviceCustomer = service;
            _serviceAddress = serviceAddress;
            _httpClient = new HttpClient();            

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        //[HttpGet]
        //public async Task<IActionResult> GetAllCustomers()
        //{
        //    try
        //    {
        //        var custormers = await _serviceCustomer.GetAllCustomers();
        //        return StatusCode(200, new ServerResponse
        //        {
        //            Message = "",
        //            Data = custormers
        //        });
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "Internal Server Error");
        //    }
        //}

        [HttpGet]
        public async Task<IActionResult> GetCustomerById()
        {
            try
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
                string id = jwt.Claims.First(c => c.Type == "nameid").Value;
                Guid.TryParse(id, out Guid userId);

                var customer = await _serviceCustomer.GetCustomerById(userId);
                if (customer != null)
                {
                    return StatusCode(200, new ServerResponse
                    {
                        Message = "",
                        Data = customer
                    });
                }
                else
                {
                    return StatusCode(404, new ServerResponse
                    {
                        Message = "",
                        Data = null
                    });
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerRequest customer)
        {
            var jwtStringTokenComplete = Request.Headers["Authorization"].ToString();
            var jwtStringToken = jwtStringTokenComplete.Replace("Bearer ", "");
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(jwtStringToken);
            string id = jwt.Claims.First(c => c.Type == "nameid").Value;
            try
            {
                if (await _serviceCustomer.ValidateDni(customer.Dni))
                {
                    var customerCreate = await _serviceCustomer.CreateCustomer(customer, id);
                    if (customerCreate != null)
                    {
                        return StatusCode(200, new ServerResponse
                        {
                            Message = "Success",
                            Data = customerCreate.Id
                        });
                }
                else
                {
                    return StatusCode(404, new ServerResponse
                    {
                        Message = "Error",
                        Data = null
                    });
                }
            }
                else
                {
                    return StatusCode(409, new ServerResponse
                    {
                        Message = "User already exists",
                        Data = null
                    });

                }

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("Address")]
        public async Task<IActionResult> CreateCustomerAddress([FromBody] AddressRequest customer)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
            string id = jwt.Claims.First(c => c.Type == "nameid").Value;
            Guid.TryParse(id, out Guid userId);
            try
            {
                //var customerExis = await _serviceCustomer.GetCustomerById(customer.CustomerId);
                //validar que exista, crear un get by customer id 

                if (true)
                {
                    var customeRespons = await _serviceAddress.CreateCustomerAddress(customer, userId);
                    if (customeRespons)
                    {
                        return StatusCode(200, new ServerResponse
                        {
                            Message = "Success",
                            Data = customeRespons
                        });
                    }
                    else
                    {
                        return StatusCode(404, new ServerResponse
                        {
                            Message = "Error",
                            Data = null
                        });
                    }
                }
                else
                {
                    return StatusCode(409, new ServerResponse
                    {
                        Message = "User does not exist",
                        Data = null
                    });
                }

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("VerificationSet")]
        public async Task<IActionResult> VerificationSet()
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
            string id = jwt.Claims.First(c => c.Type == "nameid").Value;
            Guid.TryParse(id, out Guid userId);
            try
            {
                var customerExis = await _serviceCustomer.GetCustomerById(userId);
                
                if (customerExis != null)
                {
                    if( await _serviceCustomer.VerificationSet(userId))
                    {
                        var userToken = HttpContext.Request.Headers["Authorization"].ToString();

                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken[7..]);
                        var state = new Dictionary<string, int>
                        {
                            { "State", 1 }
                        };
                        var json = JsonConvert.SerializeObject(state);
                        var data = new StringContent(json, Encoding.UTF8, "application/json");
                        Console.WriteLine(state);
                        var URL = "https://localhost:7132/api/Authentication/changeState";

                        var response = await _httpClient.PostAsync(URL, data);

                        return StatusCode(200, new ServerResponse
                        {
                            Message = "Success",
                            Data = null
                        });
                    }
                    else
                    {
                        return StatusCode(404, new ServerResponse
                        {
                            Message = "Error",
                            Data = null
                        });
                    }
                }
                    
                else
                {
                    return StatusCode(409, new ServerResponse
                    {
                        Message = "User does not exist",
                        Data = null
                    });
                }

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("VerificationState")]
        public async Task<IActionResult> VerificationState()
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
            string id = jwt.Claims.First(c => c.Type == "nameid").Value;
            Guid.TryParse(id, out Guid userId);            
            try
            {
                var customerExis = await _serviceCustomer.GetCustomerById(userId);

                if (customerExis != null)
                {
                    var customer= await _serviceCustomer.VerificationState(userId);
                    if(customer != null)
                    {
                        return StatusCode(200, new ServerResponse
                        {
                            Message = "Success",
                            Data = customer
                        });
                    }
                    else
                    {
                        return StatusCode(404, new ServerResponse
                        {
                            Message = "Unverified customer",
                            Data = null
                        });
                    }
                   
                }
                else
                {
                    return StatusCode(409, new ServerResponse
                    {
                        Message = "User does not exist",
                        Data = null
                    });
                }

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("dni")]
        public async Task<IActionResult> GetCustomerByDni(string dni)
        {
            try
            {
                var customer = await _serviceCustomer.GetCustomerByDni(dni);
                if (customer != null)
                {
                    return StatusCode(200, new ServerResponse
                    {
                        Message = "",
                        Data = customer
                    });
                }
                else
                {
                    return StatusCode(404, new ServerResponse
                    {
                        Message = "",
                        Data = null
                    });
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


    }
}
