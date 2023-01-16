using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text.Json;
using Transactions.Application.Interfaces;
using Transactions.Domain.DTOs.Request;
using Transactions.Domain.DTOs.Response;
using Transactions.Domain.Models;

namespace Transactions.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;
        private readonly HttpClient _httpClient;
        private const int OP_TYPE_TRANSF_SAME_TITULAR = 1;
        private const int OP_TYPE_TRANSF_DIFF_TITULAR = 2;
        private const int OP_TYPE_INCOM_MONEY = 3;
        private const int OP_TYPE_EXTR_MONEY = 4;
        private const string OK_RESULT = "Ok";
        private readonly string URL_ACCOUNT_FOR_PUT;
        private readonly string URL_ACCOUNT_FOR_GET;

        public TransactionController(ITransactionService service, IConfiguration configuration)
        {
            _service = service;
            _httpClient = new HttpClient();

            URL_ACCOUNT_FOR_PUT = configuration.GetSection("URL-Account-Base").Value + "/UpdateBalance";
            URL_ACCOUNT_FOR_GET = configuration.GetSection("URL-Account-Base").Value + "";
        }
        

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        [HttpPost]

        public async Task<IActionResult> AddTransaction([FromBody] TransactionsRequest model)
        {
            try
            {
                //var jwt = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
                //string decodedAccountId = jwt.Claims.First(c => c.Type == "nameid").Value;

                var userToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {userToken}");

                var FromAccountId = model.FromAccountId;

                var ToAccountId = model.ToAccountId;

                //
                // LO QUE HAY QUE TOMAR ES EL ACCOUNTID != AL USERID, ES MUCHO MUY IMPORTANTE ACORDARSE!!!
                //
                //if (model.DestinationIsByCbu)
                //{
                //    var receiver = await _httpClient.GetStringAsync($"{URL_ACCOUNT_FOR_GET_BY_CBU}/{model.ToAccount}");
                //    var receiverByCbu = JsonSerializer.Deserialize<ResponseByCbu>(receiver);
                //    ToAccountId = receiverByCbu.Data.AccountId;
                //    receiverGet = await _httpClient.GetStringAsync($"{URL_ACCOUNT_FOR_GET}/{ToAccountId}");

                //}
                //else
                //{
                //    var receiver = await _httpClient.GetStringAsync($"{URL_ACCOUNT_FOR_GET_BY_ALIAS}/{model.ToAccount}");
                //    var receiverByAlias = JsonSerializer.Deserialize<ResponseByAlias>(receiver);
                //    ToAccountId = receiverByAlias.Data.AccountId;
                //    receiverGet = await _httpClient.GetStringAsync($"{URL_ACCOUNT_FOR_GET}/{ToAccountId}");
                //}                    

                string emisorGet = await _httpClient.GetStringAsync($"{URL_ACCOUNT_FOR_GET}/{FromAccountId}");

                string receiverGet = await _httpClient.GetStringAsync($"{URL_ACCOUNT_FOR_GET}/{ToAccountId}");

                var emisorAccount = JsonSerializer.Deserialize<ResponseModel>(emisorGet);

                var receiverAccount = JsonSerializer.Deserialize<ResponseModel>(receiverGet);

                if (emisorAccount is null || receiverAccount is null)
                    return StatusCode(500, new ServerResponse
                    {
                        Message = "An error has occurred with the connection."
                    });

                string prevComprobations = OK_RESULT;

                switch (model.OperationType)
                {
                    case OP_TYPE_TRANSF_DIFF_TITULAR:
                    case OP_TYPE_TRANSF_SAME_TITULAR:
                        prevComprobations = await PrevComprobationsBetweenAccounts(emisorAccount, receiverAccount, model);
                        break;
                    case OP_TYPE_EXTR_MONEY:
                        prevComprobations = await PrevComprobationsExtraction(emisorAccount, receiverAccount, model);
                        break;
                    case OP_TYPE_INCOM_MONEY:
                        prevComprobations = await PrevComprobationsDeposit(emisorAccount, receiverAccount, model);
                        break;
                }

                if (prevComprobations != OK_RESULT)
                    return StatusCode(400, new ServerResponse
                    {
                        Message = prevComprobations
                    });

                var transaction = await _service
                    .AddTransaction(
                        FromAccountId,
                        ToAccountId,
                        model.OperationType,
                        model.Reason,
                        model.Amount,
                        1);

                if (transaction is not null)
                {
                    var updateBalanceEmisor = new UpdateBalanceModel
                    {
                        AccountId = FromAccountId,
                        Amount = new decimal(model.Amount) * -1
                    };

                    var updateBalanceReceiver = new UpdateBalanceModel
                    {
                        AccountId = ToAccountId,
                        Amount = new decimal(model.Amount)
                    };

                    var balanceIssuerResult = OK_RESULT;
                    var balanceReceiverResult = OK_RESULT;

                    switch (model.OperationType)
                    {
                        case OP_TYPE_TRANSF_DIFF_TITULAR:
                        case OP_TYPE_TRANSF_SAME_TITULAR:
                            balanceIssuerResult = await BalanceIssuer(URL_ACCOUNT_FOR_PUT, updateBalanceEmisor);
                            balanceReceiverResult = await BalanceReceiver(URL_ACCOUNT_FOR_PUT, updateBalanceReceiver);

                            // ROLLBACK DE TRX, ¿MEJORABLE?, TAL VEZ
                            if (balanceReceiverResult != OK_RESULT)
                                balanceReceiverResult = await RetryBalanceReceiver(URL_ACCOUNT_FOR_PUT, updateBalanceReceiver, updateBalanceEmisor);

                            break;
                        case OP_TYPE_EXTR_MONEY:
                            balanceIssuerResult = await BalanceIssuer(URL_ACCOUNT_FOR_PUT, updateBalanceEmisor);
                            break;
                        case OP_TYPE_INCOM_MONEY:
                            balanceReceiverResult = await BalanceReceiver(URL_ACCOUNT_FOR_PUT, updateBalanceReceiver);
                            break;
                    }

                    if (balanceIssuerResult != OK_RESULT)
                        return StatusCode(500, new ServerResponse
                        {
                            Message = balanceIssuerResult
                        });

                    if (balanceReceiverResult != OK_RESULT)
                        return StatusCode(500, new ServerResponse
                        {
                            Message = balanceReceiverResult
                        });

                    var result = await _service.AddMovementToHistory(transaction, emisorAccount.Data, receiverAccount.Data);

                    if (result)
                        return StatusCode(201, new ServerResponse
                        {
                            Message = "Transaction generated OK."
                        });
                }

                return StatusCode(400, new ServerResponse
                {
                    Message = "The transaction could not be completed, check the information input."
                });

            }
            catch (FormatException)
            {
                return StatusCode(400, new ServerResponse
                {
                    Message = "Please, check the syntax of your input."
                });
            }
            catch (HttpRequestException)
            {
                return StatusCode(500, new ServerResponse
                {
                    Message = "There were communication problems."
                });
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(401, new ServerResponse
                {
                    Message = "The user does not have authorization to operate."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new ServerResponse
                {
                    Message = "Internal server error."
                });
            }
        }

        private async Task<bool> AddMovementRejected(dynamic emisorAccount, dynamic receiverAccount, TransactionsRequest model)
        {
            return await _service
                .AddMovementRejectedToHistory(
                        new decimal(model.Amount),
                        model.OperationType,
                        model.FromAccountId,
                        model.ToAccountId,
                        emisorAccount.Data,
                        receiverAccount.Data);
        }

        private async Task<string> PrevComprobationsExtraction(dynamic emisorAccount, dynamic receiverAccount, TransactionsRequest model)
        {
            if (!_service.EmisorHasFunds(emisorAccount.Data, model.Amount))
            {
                await AddMovementRejected(emisorAccount, receiverAccount, model);

                return "The issuer's account has not enough funds to complete the transaction.";
            }

            if (!_service.EmisorIsEnabled(emisorAccount.Data))
            {
                await AddMovementRejected(emisorAccount, receiverAccount, model);

                return "The recipient's account is not enabled to operate.";
            }

            return OK_RESULT;
        }

        private async Task<string> PrevComprobationsDeposit(dynamic emisorAccount, dynamic receiverAccount, TransactionsRequest model)
        {
            if (!_service.ReceiverIsEnabled(receiverAccount.Data))
            {
                await AddMovementRejected(emisorAccount, receiverAccount, model);

                return "The recipient's account is not enabled to operate.";
            }

            return OK_RESULT;
        }

        private async Task<string> PrevComprobationsBetweenAccounts(dynamic emisorAccount, dynamic receiverAccount, TransactionsRequest model)
        {
            if (!_service.CurrencyMatch(emisorAccount.Data, receiverAccount.Data))
            {
                await AddMovementRejected(emisorAccount, receiverAccount, model);

                return "The transaction could not be completed, the currency is different in the intervening accounts.";
            }

            if (!_service.EmisorHasFunds(emisorAccount.Data, model.Amount))
            {
                await AddMovementRejected(emisorAccount, receiverAccount, model);

                return "The issuer's account has not enough funds to complete the transaction.";
            }

            if (!_service.EmisorIsEnabled(emisorAccount.Data))
            {
                await AddMovementRejected(emisorAccount, receiverAccount, model);

                return "The issuer's account is not enabled to operate.";
            }

            if (!_service.ReceiverIsEnabled(receiverAccount.Data))
            {
                await AddMovementRejected(emisorAccount, receiverAccount, model);

                return "The recipient's account is not enabled to operate.";
            }

            return OK_RESULT;
        }

        private async Task<string> BalanceIssuer(string urlAccountPut, UpdateBalanceModel updateBalanceEmisor)
        {

            var resultUpdateEmisor = await _httpClient.PutAsJsonAsync(urlAccountPut, updateBalanceEmisor);

            if (!resultUpdateEmisor.IsSuccessStatusCode)
            {
                return "The transaction could not be completed, the issuing account could not be updated.";
            }

            return OK_RESULT;
        }

        private async Task<string> BalanceReceiver(string urlAccountPut, UpdateBalanceModel updateBalanceReceiver)
        {
            var resultUpdateReceiver = await _httpClient.PutAsJsonAsync(urlAccountPut, updateBalanceReceiver);

            if (!resultUpdateReceiver.IsSuccessStatusCode)
            {
                return "The transaction could not be completed, the receiving account could not be updated.";
            }

            return OK_RESULT;
        }

        private async Task<string> RetryBalanceReceiver(string urlAccountPut, UpdateBalanceModel updateBalanceReceiver, UpdateBalanceModel updateBalanceEmisor)
        {
            int tryNumber = 0;

            string msgError = "The transaction could not be processed due to communication problems.";

            do
            {
                Thread.Sleep(2000);

                var resultUpdateReceiver = await _httpClient.PutAsJsonAsync(urlAccountPut, updateBalanceReceiver);

                if (resultUpdateReceiver.IsSuccessStatusCode)
                {
                    return OK_RESULT;
                }

                tryNumber++;

            } while (tryNumber < 3);

            updateBalanceEmisor.Amount *= -1;

            await _httpClient.PutAsJsonAsync(urlAccountPut, updateBalanceEmisor);

            return msgError;
        }
    }
}
