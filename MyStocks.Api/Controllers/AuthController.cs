using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStocks.Api.Common;
using MyStocks.Application.Abstractions;
using MyStocks.Application.Authentication;
using MyStocks.Contracts.Authentication;

namespace MyStocks.Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IJWTConfig _jwtConfig;
    public AuthController(IMediator mediator, IJWTConfig jwtConfig)
    {
        _mediator = mediator;
        _jwtConfig = jwtConfig;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("v1/login")]
    public async Task<IActionResult> UserLogin([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Email, request.password);

        var commandResult = await _mediator.Send(command);

        if (commandResult.IsFailure)
            return Responses.Error(HttpContext,commandResult.Errors.ToList());

        //todo: receber valores referente ao token da app layer 
        var response = new LoginResponse(commandResult.Value,DateTime.Now,DateTime.Now.AddHours(_jwtConfig.ExpiresInHours));
        
        return Ok(response);     
    }

}
