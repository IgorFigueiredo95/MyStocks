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
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Queries;

public class GetShareListQueryHandler : IRequestHandler<GetShareListQuery, Result<List<ShareResult>>>
{
    private readonly IShareQueryRepository _shareQueryRepository;
    private readonly IPrincipal _principal;
    private readonly IQuotationService _quotationService;

    public GetShareListQueryHandler(
        IShareQueryRepository shareQueryRepository,
        IPrincipal principal,
        IQuotationService quotationService)
    {
        _shareQueryRepository = shareQueryRepository;
        _principal = principal;
        _quotationService = quotationService;
    }

    public async Task<Result<List<ShareResult>>> Handle(GetShareListQuery request, CancellationToken cancellationToken)
    {
        var sharesQuery = await _shareQueryRepository.GetSharesList(Guid.Parse(_principal.Identity.Name), request.limit, request.offSet);

        List<ShareResult> result = new List<ShareResult>();

        foreach(var share in sharesQuery)
        {
            try
            {
                var currentPrice = await _quotationService.GetShareCurrentPrice(share.Code, cancellationToken);
                var percentage = QuotationUtils.GetVariationPercentage(currentPrice.Value, share.AveragePrice.Value);
                var variatiationResult = QuotationUtils.GetCorrespondingValue(
                        percentage, 
                        share.AveragePrice.Value, 
                        share.TotalShares.Value
                    );

                result.Add(
                    new ShareResult(
                        share.Code,
                        share.Name,
                        share.Description,
                        Enum.Parse<ShareTypes>(share.ShareType.ToString()).ToString(),
                        share.CurrencyType,
                        share.TotalValueInvested.Value,
                        share.TotalShares.Value,
                        share.AveragePrice.Value,
                        new VariationResult(currentPrice.Value, percentage, variatiationResult)
                    )
                );

            }
            catch (Exception)
            {
                //Todo: por enquanto interromper processo e retornar exception
                throw new Exception("An internal error has occurred.");
            }
        }
        return result;
    }
}
