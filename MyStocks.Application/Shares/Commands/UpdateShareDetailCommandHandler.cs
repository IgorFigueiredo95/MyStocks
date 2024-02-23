using MediatR;
using MyStocks.Domain.Abstractions;
using MyStocks.Domain.Common;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Currencies;
using MyStocks.Domain.Enums;
using MyStocks.Domain.Shares;
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
    private readonly IShareRepository _shareRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateShareDetailCommandHandler(
        IShareRepository shareRepository,
        IUnitOfWork unitOfWork)
    {
        _shareRepository = shareRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(UpdateShareDetailCommand request, CancellationToken cancellationToken)
    {
       var oldShareDetail = await _shareRepository.GetShareDetailByIdAsync(request.ShareDetailId);

        if (oldShareDetail is null)
            return Error.Create("SHAREDETAIL_NOT_FOUND", $"Share detail not found.");

        var share = await _shareRepository.GetByIdAsync(oldShareDetail!.ShareId);

        var updatedPrice =  
            (request.Price != null) ? 
                Currency.Create(oldShareDetail.Price.CurrencyType, request.Price.Value) 
                : oldShareDetail.Price;

        var updatedQuantity = request.Quantity ?? oldShareDetail.Quantity;
        
        try
        {
            share.UpdateShareDetail(oldShareDetail,request.Note, updatedQuantity, updatedPrice);
        }
        catch (Exception ex)
        {
            return Error.Create("INVALID_OPERATION", ex.Message);
        }

        _shareRepository.Update(share);

        await _unitOfWork.CommitAsync();

        return true;
    }
}
