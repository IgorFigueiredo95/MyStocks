using MediatR;
using MyStocks.Domain.Abstractions;
using MyStocks.Domain.Common;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Currencies;
using MyStocks.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Commands;

public class UpdateShareDetailCommandHandler : IRequestHandler<UpdateShareDetailCommand, Result>
{
    private readonly IShareDetailRepository _shareDetailRepository;
    private readonly IShareRepository _shareRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateShareDetailCommandHandler(
        IShareRepository shareRepository,
        IShareDetailRepository shareDetailRepository,
        IUnitOfWork unitOfWork)
    {
        _shareRepository = shareRepository;
        _shareDetailRepository = shareDetailRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(UpdateShareDetailCommand request, CancellationToken cancellationToken)
    {
       var shareDetail = await _shareDetailRepository.GetByIdAsync(request.ShareDetailId);

        if (shareDetail is null)
            return Error.Create("SHAREDETAIL_NOT_FOUND", $"Share detail not found.");

        var oldShareDetail = shareDetail.DeepCopy();

        var share = await _shareRepository.GetByIdAsync(shareDetail!.ShareId);

        Currency newPrice = shareDetail.Price;
        if (request.Price is not null)
            newPrice = Currency.Create(shareDetail.Price.CurrencyType, request.Price.Value);

        if (shareDetail.OperationType == OperationType.Sell && 
            request.Price * request.Quantity >= share.TotalValueInvested.Value)
                return Error.Create("OPERATION_TYPE_IS_INVALID", "You cannot sell more than what you have.");

        shareDetail.Update(request.Note, request.Quantity, newPrice);

        share.UpdateShareDetail(oldShareDetail, shareDetail);

        _shareDetailRepository.Update(shareDetail);
        _shareRepository.Update(share);

        await _unitOfWork.CommitAsync();

        return true;
    }
}
