using FluentValidation;
using MediatR;
using MyStocks.Application.Common;
using MyStocks.Domain.Abstractions;
using MyStocks.Domain.Common;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Shares;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Queries;

public class GetShareDetailListByCodeQueryHandler : IRequestHandler<GetShareDetailListByCodeQuery, Result<GetShareDetailListByCodeQueryDTO>>
{
    private readonly IShareRepository _shareRepository;
    private readonly IShareDetailRepository _shareDetailRepository;
    private readonly IValidator<GetShareDetailListByCodeQuery> _validator;

    public GetShareDetailListByCodeQueryHandler(
        IShareRepository shareRepository,
        IShareDetailRepository shareDetailRepository,
        IValidator<GetShareDetailListByCodeQuery> validator)
    {
        _shareRepository = shareRepository;
        _shareDetailRepository = shareDetailRepository;
        _validator = validator;
    }
    public async Task<Result<GetShareDetailListByCodeQueryDTO>> Handle(GetShareDetailListByCodeQuery request, CancellationToken cancellationToken)
    {
        var resultValidation = _validator.Validate(request);

        if (!resultValidation.IsValid)
            return resultValidation.ReturnListErrors();

        var shareHeader = await _shareRepository.GetByCodeAsync(request.Code);

        if (shareHeader is null)
            return Error.Create("SHARE_NOT_FOUND", $"Share with code '{request.Code}' was not found.");

        var shareList = await _shareDetailRepository.GetShareDetailByPagination(
                                                    shareHeader.Id,
                                                    request.Limit,
                                                    request.OffSet);

        //Map DTO manual
        var shareListDto = new List<GetShareDetailListDTO>();

        shareList
            .ForEach(share => shareListDto
            .Add(new GetShareDetailListDTO(
                share.Id,
                share.Note, 
                share.Quantity, 
                share.Price.Value,
                share.Price.CurrencyType.Code,
                share.OperandType.ToString()
                )));

        var result = new GetShareDetailListByCodeQueryDTO(
            shareHeader.Code,
            shareHeader.Name,
            shareHeader.Description,
            shareHeader.ShareType.ToString(),
            shareHeader.TotalValueInvested.Value,
            shareHeader.TotalValueInvested.CurrencyType.Code,
            shareHeader.TotalShares,
            shareHeader.AveragePrice.Value,
            shareListDto);
        
        return result;


    }
}
