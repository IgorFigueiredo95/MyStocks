using MediatR;
using MyStocks.Domain.Abstractions;
using MyStocks.Domain.Common;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Enums;
using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Commands;

public class UpdateShareCommandHandler : IRequestHandler<UpdateShareCommand, Result>
{
    private readonly IShareRepository _shareRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateShareCommandHandler(
        IShareRepository shareRepository,
        IUnitOfWork unitOfWork)
    {
        _shareRepository = shareRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateShareCommand request, CancellationToken cancellationToken)
    {
        var share = await _shareRepository.GetByCodeAsync(request.Code);

        if (share is null) 
           return Error.Create("SHARE_NOT_FOUND", $"Share with code '{request.Code}' was not found.");


        if (request.shareTypeCode is not null &&
            !Enum.TryParse(typeof(ShareTypes), request.shareTypeCode, out var shareType))
           return Error.Create("SHARE_TYPE_INVALID", $"Share type '{request.shareTypeCode}' is not valid.");

        var Type = share.ShareType;

        if (request.shareTypeCode is not null)
            Type = Enum.Parse<ShareTypes>(request.shareTypeCode);

        share.Update(request.Name, request.Description, Type);

        _shareRepository.Update(share);

        await _unitOfWork.CommitAsync();

        return Result.ReturnSuccess();
    }
}
