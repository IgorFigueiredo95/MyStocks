using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStocks.Api.Common;
using MyStocks.Application.Currencies;
using MyStocks.Application.Queries;
using MyStocks.Application.Shares;
using MyStocks.Application.Shares.Commands;
using MyStocks.Application.Shares.Commands.DeleteShare;
using MyStocks.Application.Shares.Commands.DeleteShareDetail;
using MyStocks.Application.Shares.Queries;
using MyStocks.Contracts.Shares;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Enums;
using MyStocks.Domain.Shares;
using System.Threading;

namespace MyStocks.Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class SharesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SharesController(IMediator mediator)
    {
        _mediator = mediator;
    }


    #region Share

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
    [Authorize]
    [HttpGet]
    [Route("{code}")]
    public async Task<IActionResult> GetShareByCode(string code, CancellationToken cancellationToken)
    {
        var query = new GetShareByCodeQuery(code);
        GetShareByCodeResponse response;
        var result = await _mediator.Send(query);

        if (result.IsFailure)
            return Responses.Error(HttpContext, result.Errors.ToList());

        return Ok(result.Value);
    }

    [HttpGet]
    [Route("List")]
    public async Task<IActionResult> GetShareListListPagination(
        [FromQuery] int? offSet,
        [FromQuery] int? limit,
        CancellationToken cancellationToken)
    {
        var query = new GetShareListQuery(limit, offSet);
        var result = await _mediator.Send(query);

        if (result.IsFailure)
            return Responses.Error(HttpContext, result.Errors.ToList());

        return Ok(result.Value);
    }

    [HttpPut]
    [Route("{code}")]
    public async Task<IActionResult> UpdateShare(string code, UpdateShareRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateShareCommand(
            code,
            request.Name,
            request.Description,
            request.ShareTypeCode);


           var result =  await _mediator.Send(command, cancellationToken);

        if (result.IsFailure)
            return Responses.Error(HttpContext,result.Errors.ToList());

        return Ok("Share updated");
    }

    [HttpDelete]
    [Route("{code}")]
    public async Task<IActionResult> DeleteShare(string code)
    {
        var command = new DeleteShareCommand(code);

        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return Responses.Error(HttpContext, result.Errors.ToList());

        return Ok();
    }
    #endregion

    #region ShareDetail
    [HttpPost]
    [Route("SharesDetail")]
    public async Task<IActionResult> AddShareDetail([FromBody] CreateShareDetailRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateShareDetailCommand(
            request.ShareCode,
            request.Note,
            request.Quantity,
            request.Price,
            request.OperationTypeCode);


        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return Responses.Error(HttpContext, result.Errors.ToList());

        return Ok(result.Value);
    }

    [HttpGet]
    [Route("shareDetails/{shareCode}")]
    public async Task<IActionResult> GetShareDetaiListPagination(
        [FromRoute] string shareCode,
        [FromQuery] int? offSet,
        [FromQuery] int? limit)
    {
        var query = new GetShareDetailListByCodeQuery(shareCode, offSet, limit);
        var result = await _mediator.Send(query);

        if (result.IsFailure)
            return Responses.Error(HttpContext, result.Errors.ToList());

        return Ok(result.Value);
    }

    [HttpPut]
    [Route("shareDetail/{id}")]
    public async Task<IActionResult> UpdateShareDetail([FromRoute] Guid id, [FromBody] UpdateShareDetailRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateShareDetailCommand(
            id,
            request.Note,
            request.Quantity,
            request.Price);


        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsFailure)
            return Responses.Error(HttpContext, result.Errors.ToList());

        return Ok();

    }

    [HttpDelete]
    [Route("shareDetail/{id}")]
    public async Task<IActionResult> DeleteShareDetailById([FromRoute] Guid id)
    {
        var command = new DeleteShareDetailCommand(id);

        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return Responses.Error(HttpContext, result.Errors.ToList());

        return Ok(result.Value);
    }
    #endregion

}
