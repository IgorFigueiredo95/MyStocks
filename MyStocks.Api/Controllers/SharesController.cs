using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyStocks.Api.Common;
using MyStocks.Application.Currencies;
using MyStocks.Application.Queries;
using MyStocks.Application.Shares;
using MyStocks.Application.Shares.Commands;
using MyStocks.Application.Shares.Queries;
using MyStocks.Contracts.Shares;
using MyStocks.Domain.Enums;
using MyStocks.Domain.Shares;
using System.Threading;

namespace MyStocks.Api.Controllers
{
    [Route("[Controller]")]
    public class SharesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SharesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShare([FromBody] CreateShareRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateShareCommand(
                request.code,
                request.name,
                request.description,
                request.shareTypeCode,
                request.currencyTypeCode);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
                return Responses.Error(HttpContext, result.Errors.ToList());
               

            return Ok(result.Value);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateShare( Guid id, UpdateShareRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateShareCommand(
                id,
                request.Name,
                request.Description,
                request.ShareTypeCode);

            try
            {
                await _mediator.Send(command, cancellationToken);
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails()
                {
                    Title = "One or more errors has ocourred.",
                    Detail = ex.Message,
                });
            }

            return Ok();

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetShareById(Guid id, CancellationToken cancellationToken) 
        {
            var query = new GetShareByIdQuery(id);
            GetShareByIdResponse response;
            try
            {
                var result = await _mediator.Send(query);
                response = new GetShareByIdResponse(
                    result.Code,
                    result.Name,
                    result.Description,
                    result.ShareType,
                    result.TotalValueInvested,
                    result.TotalShares,
                    result.AveragePrice,
                    result.SharesDetails,
                    result.CreatedAt,
                    result.UpdatedAt);

            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails()
                {
                    Title = "One or more errors has ocourred.",
                    Detail = "A validation error error has ocourred. verify you request"
                   //new Dictionary<string, string>().Add("error", $"{ex.Message}")
                });
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("SharesDetail")]
        public async Task<IActionResult> AddShareDetail(CreateShareDetailRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateShareDetailCommand(
                request.ShareCode,
                request.Note,
                request.Quantity,
                request.Price,
                request.OperationTypeCode);

            Guid result;
            try
            {
                result = await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                //todo: Estruturar a saída de erros e responses corretamente de todos endpoints
                return BadRequest(new ProblemDetails()
                {
                    Title = "One or more errors has ocourred.",
                    Detail = $"A validation error error has ocourred. verify you request. {ex.Message}"
                }); ;
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("shareDetail/{shareCode}")]
        public async Task<IActionResult> GetShareDetaiListPagination([FromRoute] string shareCode, [FromQuery] int offSet, [FromQuery] int limit) 
        {
            var query = new GetShareDetailListByCodeQuery(shareCode, offSet, limit);
            var result = await _mediator.Send(query);

            if (result.IsFailure)
                return Responses.Error(HttpContext, result.Errors.ToList());

            return Ok(result.Value);
        }
    }
}
