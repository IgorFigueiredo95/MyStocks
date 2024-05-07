using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStocks.Api.Common;
using MyStocks.Application.Abstractions;
using MyStocks.Application.Authentication;
using MyStocks.Application.Services.Quotation;
using MyStocks.Contracts.Authentication;

namespace MyStocks.Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IJWTConfig _jwtConfig;
    private readonly IQuotationService _quotationService;
    public AuthController(IMediator mediator, IJWTConfig jwtConfig, IQuotationService quotationService)
    {
        _mediator = mediator;
        _jwtConfig = jwtConfig;
        _quotationService = quotationService;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("v1/login")]
    public async Task<IActionResult> UserLogin([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {

        var command = new LoginCommand(request.Email, request.password);

        var commandResult = await _mediator.Send(command);

        if (commandResult.IsFailure)
            return Responses.ErrorResponse(HttpContext, commandResult.Errors.ToList());

        //todo: receber valores referente ao token da app layer 
        var response = new LoginResponse(commandResult.Value, DateTime.Now, DateTime.Now.AddHours(_jwtConfig.ExpiresInHours));

        return Ok(response);
    }

}
