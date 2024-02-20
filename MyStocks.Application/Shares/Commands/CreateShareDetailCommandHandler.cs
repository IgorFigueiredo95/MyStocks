using MediatR;
using MyStocks.Domain.Abstractions;
using MyStocks.Domain.Currencies;
using MyStocks.Domain.Enums;
using MyStocks.Domain.Shares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Commands;
public class CreateShareDetailCommandHandler : IRequestHandler<CreateShareDetailCommand, Guid>
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

    public async Task<Guid> Handle(CreateShareDetailCommand request, CancellationToken cancellationToken)
    {
        var share = await _shareRepository.GetByCodeAsync(request.ShareCode);
        if (share is null)
            throw new KeyNotFoundException($"No share was found with code '{request.ShareCode}'.");

        //TODO: Alterar o value object para receber somente o código do CurrencyType. está dificil trabalhar com o Currency necessitando de Id a todo moment 
        var currencyType = share.AveragePrice.CurrencyType;
        
        var shareDetail = ShareDetail.Create(
            share.Id,
            request.Quantity,
            Currency.Create(currencyType, request.Price),
            request.OperationTypeCode,
            request.Note);

        share.AddShareDetail(shareDetail);

        await _shareDetailRepository.AddAsync(shareDetail);
        _shareRepository.Update(share);
        await _unitOfWork.CommitAsync();

        return shareDetail.Id;

    }
}
