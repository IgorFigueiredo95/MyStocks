using MediatR;
using Microsoft.Win32.SafeHandles;
using MyStocks.Domain.Abstractions;
using MyStocks.Domain.Common;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.SharesAggregate.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Commands.DeleteShareDetail;

public class DeleteShareDetailCommandHandler : IRequestHandler<DeleteShareDetailCommand, Result<DeleteShareDetailResponseDTO>>
{
    private readonly IShareRepository _shareRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteShareDetailCommandHandler(IShareRepository shareRepository, IUnitOfWork unitOfWork)
    {
        _shareRepository = shareRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<DeleteShareDetailResponseDTO>> Handle(DeleteShareDetailCommand request, CancellationToken cancellationToken)
    {
        var share = await _shareRepository.GetShareByShareDetailIdAsync(request.ShareDetailId);

        if (share is null)
            return Error.Create("SHAREDETAIL_NOT_FOUND", "Share Detail not found.");

        var shareDetail = share.ShareDetails.First(s => s.Id == request.ShareDetailId);

        try
        {
            share.RemoveShareDetail(shareDetail);
            var shareDeletedEvent = new ShareDeletedDomainEvent();
            share.AddDomainEvent(shareDeletedEvent);
            _unitOfWork.DispatchDomainEvents(share.RaisedEvents);
            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            return Error.Create("INVALID_OPERATION", ex.Message);
        }

        var updatedShareResponse = new UpdatedShareResponse(
            share.Code,
            share.ShareType.ToString(),
            share.TotalValueInvested.Value,
            share.TotalShares,
            share.AveragePrice.Value);

        var response = new DeleteShareDetailResponseDTO(
            "Share detail deleted successfully.",
            updatedShareResponse);

        return response;
    }
}
