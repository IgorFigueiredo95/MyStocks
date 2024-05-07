using MediatR;
using MyStocks.Application.Queries;
using MyStocks.Application.Quotation;
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

public class GetShareByCodeQueryHandler : IRequestHandler<GetShareByCodeQuery, Result<ShareResult>>
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

    public async Task<Result<ShareResult>> Handle(GetShareByCodeQuery request, CancellationToken cancellationToken)
    {

        var shareQuery = await _ShareQueryRepository.GetShareByCode(Guid.Parse(_principal.Identity.Name), request.Code);

        if (shareQuery is null)
            return Error.Create("SHARE_NOT_FOUND", $"Share with code '{request.Code}'Not Found");
        try
        {
            var currentPrice = await _quotationService.GetShareCurrentPrice(request.Code, cancellationToken);
            var percentage = QuotationUtils.GetVariationPercentage(currentPrice.Value, shareQuery.AveragePrice.Value);
            var variatiationResult =QuotationUtils.GetCorrespondingValue(
                    percentage, 
                    shareQuery.AveragePrice.Value, 
                    shareQuery.TotalShares.Value
                );

            return new ShareResult
            (
                shareQuery.Code,
                shareQuery.Name,
                shareQuery.Description,
                Enum.Parse<ShareTypes>(shareQuery.ShareType.ToString()).ToString(),
                shareQuery.CurrencyType,
                shareQuery.TotalValueInvested.Value,
                shareQuery.TotalShares.Value,
                shareQuery.AveragePrice.Value,
                new VariationResult(currentPrice.Value, percentage, variatiationResult)
            );
        }
        catch (Exception)
        {

            throw;
        }
    }
}
