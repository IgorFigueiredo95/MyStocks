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
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Commands;
public class CreateShareDetailCommandHandler : IRequestHandler<CreateShareDetailCommand, Result<Guid>>
{
    private readonly IShareRepository _shareRepository;
    private readonly IShareDetailRepository _shareDetailRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateShareDetailCommandHandler(
        IShareRepository shareRepository,
        IUnitOfWork unitOfWork,
        IShareDetailRepository shareDetailRepository)
    {
        _shareRepository = shareRepository;
        _unitOfWork = unitOfWork;
        _shareDetailRepository = shareDetailRepository;
    }

    public async Task<Result<Guid>> Handle(CreateShareDetailCommand request, CancellationToken cancellationToken)
    {

        var share = await _shareRepository.GetByCodeAsync(request.ShareCode);
        if (share is null)
            return Error.Create("SHARE_NOT_FOUND",$"No share was found with code '{request.ShareCode}'.");

        if (request.OperationTypeCode == OperationType.Sell.ToString() && 
            !share.HasEnoughBalanceToSell(request.Quantity, request.Price))
            return Error.Create("INVALID_OPERATION", $" You do not have enough balance to sell this amount.");

        //TODO: Alterar o value object para receber somente o código do CurrencyType. está dificil trabalhar com o Currency necessitando de Id a todo moment 
        var currencyType = share.AveragePrice.CurrencyType;
        
        var shareDetail = ShareDetail.Create(
            share.Id,
            request.Quantity,
            Currency.Create(currencyType, request.Price),
            request.OperationTypeCode,
            request.Note);

        if (request.OperationTypeCode == OperationType.Buy.ToString())
        {
            share.AddValues(shareDetail);
        }
        else
        {
            share.RemoveValues(shareDetail);
        }
        

        await _shareDetailRepository.AddAsync(shareDetail);
        _shareRepository.Update(share);

        await _unitOfWork.CommitAsync();

        return shareDetail.Id;

    }
}
