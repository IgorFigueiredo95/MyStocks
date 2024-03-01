using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyStocks.Api.Common;
using MyStocks.Application.Portfolios.Commands;
using MyStocks.Application.Portfolios.Commands.AddShareToPortfolio;
using MyStocks.Contracts;
using MyStocks.Contracts.Portfolio;

namespace MyStocks.Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class PortfolioController : ControllerBase
{
    private readonly IMediator _mediator;
    public PortfolioController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePortfolio([FromBody] CreatePortfolioRequest request, CancellationToken cancellation)
    {
        var command = new CreatePortfolioCommand(request.Name, request.Description);

        var commandResult = await _mediator.Send(command);

        if (commandResult.IsFailure)
            return Responses.Error(HttpContext, commandResult.Errors.ToList());

        return Ok(commandResult.Value);
    }

    [HttpPost]
    [Route("Share/{PortfolioId}")]
    public async Task<IActionResult> AddShare([FromRoute] Guid PortfolioId, [FromBody] AddShareToPortfolioRequest request)
    {
        var command = new AddShareToPortfolioCommand(PortfolioId, request.ShareCode);

        var commandResult = await _mediator.Send(command);

        if(commandResult.IsFailure)
            return Responses.Error(HttpContext,commandResult.Errors.ToList());

        return Ok();
    }
}
