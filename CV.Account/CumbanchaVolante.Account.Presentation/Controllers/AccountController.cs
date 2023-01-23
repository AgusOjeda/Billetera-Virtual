using CV.MsAccount.Application.Interfaces;
using CV.MsAccount.Application.Response;
using CV.MsAccount.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CV.MsAccount.Presentation.Controllers
{
    [Route("api/account")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly HttpClient _httpClient;

        public AccountController(IAccountService service)
        {
            _service = service;
            _httpClient = new HttpClient();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var jwtStringTokenComplete = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson;
            try
            {
                if (!ModelState.IsValid)
                {
                    var errorResponse = new ServerResponse
                    {
                        Message = "Not a valid account id!",
                    };
                    strJson = JsonSerializer.Serialize(errorResponse, opt);
                    return StatusCode(StatusCodes.Status400BadRequest, strJson);
                }

                var result = await _service.GetById(id, jwtStringTokenComplete);

                if (result is null)
                {
                    var errorResponse = new ServerResponse
                    {
                        Message = "Account not found!",
                    };
                    strJson = JsonSerializer.Serialize(errorResponse, opt);
                    return StatusCode(StatusCodes.Status404NotFound, strJson);
                }
                var response = new ServerResponse
                {
                    Message = "Account found!",
                    Data = result
                };
                strJson = JsonSerializer.Serialize(response, opt);
                return Ok(strJson);
            }
            catch (FormatException)
            {
                var errorResponse = new ServerResponse
                {
                    Message = "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)",
                };
                string strJsonError = JsonSerializer.Serialize(errorResponse, opt);
                return StatusCode(StatusCodes.Status400BadRequest, strJsonError);
            }
            catch (Exception)
            {
                var errorResponse = new ServerResponse
                {
                    Message = "Server error!",
                };
                string strJsonError = JsonSerializer.Serialize(errorResponse, opt);
                return StatusCode(StatusCodes.Status500InternalServerError, strJsonError);
            }
        }

        [HttpGet("customerId")]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCustomerId()
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
            string id = jwt.Claims.First(c => c.Type == "nameid").Value;

            Guid.TryParse(id, out Guid userId);

            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson;
            try
            {
                if (userId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    var errorResponse = new ServerResponse
                    {
                        Message = "Not a valid customer id!",
                    };
                    strJson = JsonSerializer.Serialize(errorResponse, opt);
                    return StatusCode(StatusCodes.Status400BadRequest, strJson);
                }

                var result = await _service.GetByCustomerId(userId);

                if (result is null)
                {
                    var errorResponse = new ServerResponse
                    {
                        Message = "The customer does not exists!",
                    };
                    strJson = JsonSerializer.Serialize(errorResponse, opt);
                    return StatusCode(StatusCodes.Status404NotFound, strJson);
                }
                if (result.Count == 0)
                {
                    var errorResponse = new ServerResponse
                    {
                        Message = "The customer does not have accounts!",
                        Data = result
                    };
                    strJson = JsonSerializer.Serialize(errorResponse, opt);
                    return Ok(strJson);
                }
                var response = new ServerResponse
                {
                    Message = "Accounts found!",
                    Data = result
                };
                strJson = JsonSerializer.Serialize(response, opt);
                return Ok(strJson);
            }
            catch (FormatException)
            {
                var errorResponse = new ServerResponse
                {
                    Message = "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)",
                };
                string strJsonError = JsonSerializer.Serialize(errorResponse, opt);
                return StatusCode(StatusCodes.Status400BadRequest, strJsonError);
            }
            catch (Exception)
            {
                var errorResponse = new ServerResponse
                {
                    Message = "Server error!",
                };
                string strJsonError = JsonSerializer.Serialize(errorResponse, opt);
                return StatusCode(StatusCodes.Status500InternalServerError, strJsonError);
            }
        }

        [HttpGet("searchByCbu")]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCbu([FromQuery] string cbu)
        {
            var userToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer ", userToken);


            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson;
            try
            {
                if (!ModelState.IsValid)
                {
                    var errorResponse = new ServerResponse
                    {
                        Message = "Not a valid account cbu!",
                    };
                    strJson = JsonSerializer.Serialize(errorResponse, opt);
                    return StatusCode(StatusCodes.Status400BadRequest, strJson);
                }

                var result = await _service.GetByCbu(cbu);

                if (result is null)
                {
                    var errorResponse = new ServerResponse
                    {
                        Message = "Account not found. Please, check the cbu!",
                    };
                    strJson = JsonSerializer.Serialize(errorResponse, opt);
                    return StatusCode(StatusCodes.Status404NotFound, strJson);
                }
                var response = new ServerResponse
                {
                    Message = "Account found!",
                    Data = result
                };
                strJson = JsonSerializer.Serialize(response, opt);
                return Ok(strJson);
            }
            catch (Exception)
            {
                var errorResponse = new ServerResponse
                {
                    Message = "Server error!",
                };
                string strJsonError = JsonSerializer.Serialize(errorResponse, opt);
                return StatusCode(StatusCodes.Status500InternalServerError, strJsonError);
            }
        }

        [HttpGet("searchByAlias")]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByAlias([FromQuery] string alias)
        {
            var userToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer ", userToken);


            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson;
            try
            {
                if (!ModelState.IsValid)
                {
                    var errorResponse = new ServerResponse
                    {
                        Message = "Not a valid account alias!",
                    };
                    strJson = JsonSerializer.Serialize(errorResponse, opt);
                    return StatusCode(StatusCodes.Status400BadRequest, strJson);
                }

                var result = await _service.GetByAlias(alias);

                if (result is null)
                {
                    var errorResponse = new ServerResponse
                    {
                        Message = "Account not found. Please, check the alias!",
                    };
                    strJson = JsonSerializer.Serialize(errorResponse, opt);
                    return StatusCode(StatusCodes.Status404NotFound, strJson);
                }
                var response = new ServerResponse
                {
                    Message = "Account found!",
                    Data = result
                };
                strJson = JsonSerializer.Serialize(response, opt);
                return Ok(strJson);
            }
            catch (Exception)
            {
                var errorResponse = new ServerResponse
                {
                    Message = "Server error!",
                };
                string strJsonError = JsonSerializer.Serialize(errorResponse, opt);
                return StatusCode(StatusCodes.Status500InternalServerError, strJsonError);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> CreateAccount([FromQuery] string shortNameCurrency)
        {
            var jwtStringTokenComplete = Request.Headers["Authorization"].ToString();
            var jwtStringToken = jwtStringTokenComplete.Replace("Bearer ", "");
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(jwtStringToken);
            string id = jwt.Claims.First(c => c.Type == "nameid").Value;            
            Guid.TryParse(id, out Guid userId);

            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson;

            try
            {
                if (!ModelState.IsValid)
                {
                    var errorResponse = new ServerResponse
                    {
                        Message = "Invalid input Data!",
                    };
                    strJson = JsonSerializer.Serialize(errorResponse, opt);
                    return StatusCode(StatusCodes.Status400BadRequest, strJson);
                }

                var result = await _service.CreateAccount(userId, shortNameCurrency);

                if (result is null)
                {
                    var errorResponse = new ServerResponse
                    {
                        Message = "Account not created!",
                    };
                    strJson = JsonSerializer.Serialize(errorResponse, opt);
                    return BadRequest(strJson);
                }

                var response = new ServerResponse
                {
                    Message = "Account created!",
                    Data = result
                };
                strJson = JsonSerializer.Serialize(response, opt);
                return StatusCode(StatusCodes.Status201Created, strJson);

            }
            catch (FormatException)
            {
                var errorResponse = new ServerResponse
                {
                    Message = "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)",
                };
                string strJsonError = JsonSerializer.Serialize(errorResponse, opt);
                return StatusCode(StatusCodes.Status400BadRequest, strJsonError);
            }
            catch (Exception)
            {
                var errorResponse = new ServerResponse
                {
                    Message = "Server error!",
                };
                string strJsonError = JsonSerializer.Serialize(errorResponse, opt);
                return StatusCode(StatusCodes.Status500InternalServerError, strJsonError);
            }
        }

        [HttpPut("UpdateBalance")]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> UpdateAccountBalance([FromBody] UpdateBalanceRequest updateBalanceRequest)
        {
            var jwtStringTokenComplete = Request.Headers["Authorization"].ToString();
            var jwtStringToken = jwtStringTokenComplete.Replace("Bearer ", "");
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(jwtStringToken);
            string id = jwt.Claims.First(c => c.Type == "nameid").Value;
            Guid.TryParse(id, out Guid userId);

            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson;
            try
            {
                var result = await _service.UpdateAccountBalance(updateBalanceRequest.AccountId, updateBalanceRequest.Amount);
                if (result is null)
                {
                    var errorResponse = new ServerResponse
                    {
                        Message = "Could not balance the account!",
                    };
                    strJson = JsonSerializer.Serialize(errorResponse, opt);
                    return StatusCode(StatusCodes.Status400BadRequest, strJson);
                }
                var response = new ServerResponse
                {
                    Message = "Account balance made!",
                    Data = result
                };
                strJson = JsonSerializer.Serialize(response, opt);
                return Ok(strJson);
            }
            catch (FormatException)
            {
                var errorResponse = new ServerResponse
                {
                    Message = "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)",
                };
                string strJsonError = JsonSerializer.Serialize(errorResponse, opt);
                return StatusCode(StatusCodes.Status400BadRequest, strJsonError);
            }
            catch (Exception)
            {
                var errorResponse = new ServerResponse
                {
                    Message = "Server error!",
                };
                string strJsonError = JsonSerializer.Serialize(errorResponse, opt);
                return StatusCode(StatusCodes.Status500InternalServerError, strJsonError);
            }

        }

        [HttpPut("UpdateAlias")]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> UpdateAccountAlias([FromQuery] Guid accountId, string alias)
        {
            var jwtStringTokenComplete = Request.Headers["Authorization"].ToString();


            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson;
            try
            {
                var account = await _service.GetById(accountId, jwtStringTokenComplete);
                if (account is null)
                {
                    var errorResponse = new ServerResponse
                    {
                        Message = "Account not found!",
                    };
                    strJson = JsonSerializer.Serialize(errorResponse, opt);
                    return StatusCode(StatusCodes.Status404NotFound, strJson);
                }

                var result = await _service.UpdateAccountAlias(accountId, alias);
                if (result is null)
                {
                    var errorResponse = new ServerResponse
                    {
                        Message = "Could not updated the alias!",
                    };
                    strJson = JsonSerializer.Serialize(errorResponse, opt);
                    return StatusCode(StatusCodes.Status400BadRequest, strJson);
                }
                var response = new ServerResponse
                {
                    Message = "New alias for the account!",
                    Data = result
                };
                strJson = JsonSerializer.Serialize(response, opt);
                return Ok(strJson);
            }
            catch (FormatException)
            {
                var errorResponse = new ServerResponse
                {
                    Message = "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)",
                };
                string strJsonError = JsonSerializer.Serialize(errorResponse, opt);
                return StatusCode(StatusCodes.Status400BadRequest, strJsonError);
            }
            catch (Exception)
            {
                var errorResponse = new ServerResponse
                {
                    Message = "Server error!",
                };
                string strJsonError = JsonSerializer.Serialize(errorResponse, opt);
                return StatusCode(StatusCodes.Status500InternalServerError, strJsonError);
            }

        }

        [HttpPut("UpdateState")]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAccountState([FromQuery] Guid accountId, string state)
        {
            var jwtStringTokenComplete = Request.Headers["Authorization"].ToString();



            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson;
            try
            {
                var account = await _service.GetById(accountId, jwtStringTokenComplete);
                if (account is null)
                {
                    var errorResponse = new ServerResponse
                    {
                        Message = "Account not found!",
                    };
                    strJson = JsonSerializer.Serialize(errorResponse, opt);
                    return StatusCode(StatusCodes.Status404NotFound, strJson);
                }

                var result = await _service.UpdateAccountState(accountId, state);
                if (result is null)
                {
                    var errorResponse = new ServerResponse
                    {
                        Message = "Could not update the account state!",
                    };
                    strJson = JsonSerializer.Serialize(errorResponse, opt);
                    return StatusCode(StatusCodes.Status400BadRequest, strJson);
                }
                var response = new ServerResponse
                {
                    Message = "Account state updated!",
                    Data = result
                };
                strJson = JsonSerializer.Serialize(response, opt);
                return Ok(strJson);
            }
            catch (FormatException)
            {
                var errorResponse = new ServerResponse
                {
                    Message = "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)",
                };
                string strJsonError = JsonSerializer.Serialize(errorResponse, opt);
                return StatusCode(StatusCodes.Status400BadRequest, strJsonError);
            }
            catch (Exception)
            {
                var errorResponse = new ServerResponse
                {
                    Message = "Server error!",
                };
                string strJsonError = JsonSerializer.Serialize(errorResponse, opt);
                return StatusCode(StatusCodes.Status500InternalServerError, strJsonError);
            }

        }

        [HttpPut("UpdateCurrency")]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAccountCurrency([FromQuery] Guid accountId, string shortNameCurrency)
        {
            var jwtStringTokenComplete = Request.Headers["Authorization"].ToString();



            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson;
            try
            {
                var account = await _service.GetById(accountId, jwtStringTokenComplete);
                if (account is null)
                {
                    var errorResponse = new ServerResponse
                    {
                        Message = "Account not found!",
                    };
                    strJson = JsonSerializer.Serialize(errorResponse, opt);
                    return StatusCode(StatusCodes.Status404NotFound, strJson);
                }

                var result = await _service.UpdateAccountCurrency(accountId, shortNameCurrency);
                if (result is null)
                {
                    var errorResponse = new ServerResponse
                    {
                        Message = "Could not update the account currency! Check the name of the currency.",
                    };
                    strJson = JsonSerializer.Serialize(errorResponse, opt);
                    return StatusCode(StatusCodes.Status400BadRequest, strJson);
                }
                var response = new ServerResponse
                {
                    Message = "Account currency updated!",
                    Data = result
                };
                strJson = JsonSerializer.Serialize(response, opt);
                return Ok(strJson);
            }
            catch (FormatException)
            {
                var errorResponse = new ServerResponse
                {
                    Message = "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)",
                };
                string strJsonError = JsonSerializer.Serialize(errorResponse, opt);
                return StatusCode(StatusCodes.Status400BadRequest, strJsonError);
            }
            catch (Exception)
            {
                var errorResponse = new ServerResponse
                {
                    Message = "Server error!",
                };
                string strJsonError = JsonSerializer.Serialize(errorResponse, opt);
                return StatusCode(StatusCodes.Status500InternalServerError, strJsonError);
            }

        }

        [HttpDelete("DeleteAccount")]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAccount(Guid accountId)
        {
            var jwtStringTokenComplete = Request.Headers["Authorization"].ToString();



            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson;
            try
            {
                var account = await _service.GetById(accountId, jwtStringTokenComplete);
                if (account is null)
                {
                    var errorResponse = new ServerResponse
                    {
                        Message = "Account not found. Check the entered account id",
                    };
                    strJson = JsonSerializer.Serialize(errorResponse, opt);
                    return StatusCode(StatusCodes.Status404NotFound, strJson);
                }

                var result = await _service.DeleteAccount(accountId);

                if (result is null)
                {
                    var errorResponse = new ServerResponse
                    {
                        Message = "Account not deleted!",
                    };
                    strJson = JsonSerializer.Serialize(errorResponse, opt);
                    return BadRequest(strJson);
                }
                var response = new ServerResponse
                {
                    Message = "Account deleted successfully!",
                    Data = result
                };
                strJson = JsonSerializer.Serialize(response, opt);
                return Ok(strJson);
            }
            catch (FormatException)
            {
                var errorResponse = new ServerResponse
                {
                    Message = "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)",
                };
                string strJsonError = JsonSerializer.Serialize(errorResponse, opt);
                return StatusCode(StatusCodes.Status400BadRequest, strJsonError);
            }
            catch (Exception)
            {
                var errorResponse = new ServerResponse
                {
                    Message = "Server error!",
                };
                string strJsonError = JsonSerializer.Serialize(errorResponse, opt);
                return StatusCode(StatusCodes.Status500InternalServerError, strJsonError);
            }
        }
    }
}