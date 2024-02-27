using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyStocks.Api.Common;
using MyStocks.Application.Portfolios.Commands;
using MyStocks.Contracts;

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

        var CommandResult = await _mediator.Send(command);

        if (CommandResult.IsFailure)
            return Responses.Error(HttpContext,CommandResult.Errors.ToList());

        return Ok(CommandResult.Value);
    }

}
