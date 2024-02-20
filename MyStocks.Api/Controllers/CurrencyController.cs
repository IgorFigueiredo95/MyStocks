using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyStocks.Api.Common;
using MyStocks.Application.Currencies;
using MyStocks.Application.CurrenciesTypes;
using MyStocks.Application.CurrenciesTypes.Queries;
using MyStocks.Contracts.Currencies;
using MyStocks.Domain.Currencies;
using MyStocks.Domain.Exceptions;
using Npgsql;
using System.ComponentModel.DataAnnotations;

namespace MyStocks.Api
{
    [ApiController]
    [Route("[Controller]/currencyTypes")]
    public class CurrencyController : ControllerBase
    {

        private readonly IMediator _mediator;
        public CurrencyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCurrencyType(CreateCurrencyTypeRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateCurrencyTypeCommand(
                request.Code,
                request.CurrencyCode,
                request.Name);

            var resultValue = await _mediator.Send(command);

            if (resultValue.IsFailure)
                return Responses.Error(HttpContext, resultValue.Errors.ToList());

            return Ok(resultValue);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurrencyType([FromRoute] Guid id, [FromBody] UpdateCurrencyTypeNameRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateCurrencyTypeCommand(id, request.Name);

            var resultValue = await _mediator.Send(command);

            if (resultValue.IsFailure)
                return Responses.Error(HttpContext, resultValue.Errors.ToList());

            return Ok(resultValue);
        }
 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurrencyTypeById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetCurrencyTypeByIdQuery(id);
            
               var  resultValue = await _mediator.Send(query);

            if (resultValue.IsFailure)
                return Responses.Error(HttpContext, resultValue.Errors.ToList());

            return Ok(resultValue);

        }
    }
}
