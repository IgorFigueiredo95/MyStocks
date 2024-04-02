using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MyStocks.Api.Common;
using MyStocks.Application.Users.Commands;
using MyStocks.Contracts.Users;

namespace MyStocks.Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("v1")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellation)
    {
        var command = new CreateUserCommand(request.FirstName, request.LastName, request.Email, request.Password);

        var result = await _mediator.Send(command);

        if(result.IsFailure)
            return Responses.Error(HttpContext,result.Errors.ToList());

        return Ok(result);
    }
}
