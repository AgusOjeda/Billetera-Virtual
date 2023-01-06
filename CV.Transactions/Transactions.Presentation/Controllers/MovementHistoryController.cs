using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Transactions.Application.Interfaces;
using Transactions.Domain.DTOs.Request;
using Transactions.Domain.DTOs.Response;

namespace Transactions.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class MovementHistoryController : ControllerBase
    {
        private readonly IMovementHistoryService _service;

        public MovementHistoryController(IMovementHistoryService service)
        {
            _service = service;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        [HttpPost("all")]

        public async Task<IActionResult> GetAllMovementsByAccountId([FromBody] MovementsRequest model)
        {
            try
            {
                var movements = await _service.GetMovementsByAccountId(model.AccountId);

                if (movements is null)
                    return StatusCode(404, new ServerResponse
                    {
                        Message = "The specified Account doesn't exists."
                    });

                if (movements.Count != 0)
                    return StatusCode(200, new ServerResponse
                    {
                        Message = "Movements found successfuly.",
                        Data = movements
                    });

                return StatusCode(404, new ServerResponse
                {
                    Message = "No movements were found for the specified Account Id."
                });
            }
            catch (FormatException)
            {
                return StatusCode(400, new ServerResponse
                {
                    Message = "The information provided is incorrect."
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        [HttpPost("date")]

        public async Task<IActionResult> GetMovementsByDay([FromBody] MovementsByDayRequest model)
        {
            try
            {
                var movements = await _service
                    .GetMovementsByDay(model.AccountId, new DateTime(model.Year, model.Month, model.Day));

                if (movements is null)
                    return StatusCode(404, new ServerResponse
                    {
                        Message = "The specified Account doesn't exists."
                    });

                if (movements.Count != 0)
                    return StatusCode(200, new ServerResponse
                    {
                        Message = "Movements found successfuly.",
                        Data = movements
                    });

                return StatusCode(404, new ServerResponse
                {
                    Message = "No movements were found at date for the specified Account Id."
                });
            }
            catch (FormatException)
            {
                return StatusCode(400, new ServerResponse
                {
                    Message = "The information provided is incorrect."
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
    }
}
