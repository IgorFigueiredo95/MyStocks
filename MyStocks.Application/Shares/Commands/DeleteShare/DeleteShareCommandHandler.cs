﻿using MediatR;
using MyStocks.Domain.Abstractions;
using MyStocks.Domain.Common;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.SharesAggregate.DomainEvents;
using MyStocks.Domain.SharesAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Commands.DeleteShare;

public class DeleteShareCommandHandler : IRequestHandler<DeleteShareCommand, Result>
{
    private readonly IShareRepository _shareRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteShareCommandHandler(IShareRepository shareRepository, IUnitOfWork unitOfWork)
    {
        _shareRepository = shareRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteShareCommand request, CancellationToken cancellationToken)
    {
        var share = await _shareRepository.GetByIdAsync(request.ShareId);

        if (share is null)
            return Error.Create("SHARE_NOT_FOUND", $"a share with an id '{request.ShareId}' was not found.");

        _shareRepository.Remove(share);
        var shareDeletedEvent = new ShareDeleted(ShareId.Create(request.ShareId));
        share.AddDomainEvent(shareDeletedEvent);

        await _unitOfWork.DispatchDomainEventsAsync(share.RaisedEvents);
        await _unitOfWork.CommitAsync();

        return Result.ReturnSuccess();
    }
}
