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

        var updatedPrice =  
            (request.Price != null) ? 
                Currency.Create(shareDetail.Price.CurrencyType, request.Price.Value) 
                : shareDetail.Price;

        var updatedQuantity = request.Quantity ?? shareDetail.Quantity;

        shareDetail.Update(request.Note, updatedQuantity, updatedPrice);
        
        try
        {
            share.UpdateValues(oldShareDetail, shareDetail);
        }
        catch (Exception ex)
        {
            return Error.Create("INVALID_OPERATION", ex.Message);
        }


        _shareDetailRepository.Update(shareDetail);
        _shareRepository.Update(share);

        await _unitOfWork.CommitAsync();

        return true;
    }
}
