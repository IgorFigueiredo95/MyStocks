using MediatR;
using MyStocks.Application.Queries;
using MyStocks.Application.Services.Quotation;
using MyStocks.Domain.Abstractions;
using MyStocks.Domain.Common;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Enums;
using MyStocks.Domain.Shares;
using MyStocks.Domain.SharesAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Queries;

public class GetShareByCodeQueryHandler : IRequestHandler<GetShareByCodeQuery, Result<GetShareResponse>>
{
    private readonly IShareQueryRepository _ShareQueryRepository;
    private readonly IPrincipal _principal;
    private readonly IQuotationService _quotationService;

    public GetShareByCodeQueryHandler(
        IShareQueryRepository shareQueryRepository, 
        IPrincipal principal, 
        IQuotationService quotationService)
    {
        _ShareQueryRepository = shareQueryRepository;
        _principal = principal;
        _quotationService = quotationService;
    }

    public async Task<Result<GetShareResponse>> Handle(GetShareByCodeQuery request, CancellationToken cancellationToken)
    {

        var share = await _ShareQueryRepository.GetShareByCode(Guid.Parse(_principal.Identity.Name), request.Code);

        if (share is null)
            return Error.Create("SHARE_NOT_FOUND", $"Share with code '{request.Code}'Not Found");

        share.ShareType = (share.ShareType != null) ? Enum.Parse(typeof(ShareTypes), share.ShareType!).ToString() : "";

        var currentPrice = await _quotationService.GetQuotationDataAsync(request.Code);

        var variation = ((currentPrice / share.AveragePrice) - 1);

        var quotation = new VariationResponse(variation * 100, share.TotalValueInvested.Value * variation);

        var result = new GetShareResponse(share, quotation);
        return result;
    }
}
