using MediatR;
using MyStocks.Application.Queries;
using MyStocks.Domain.Abstractions;
using MyStocks.Domain.Common;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Enums;
using MyStocks.Domain.Shares;
using MyStocks.Domain.SharesAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Queries;

public class GetShareListQueryHandler : IRequestHandler<GetShareListQuery, Result<List<ShareDTO>>>
{
    private readonly IShareQueryRepository _shareQueryRepository;

    public GetShareListQueryHandler(IShareQueryRepository shareQueryRepository)
    {
        _shareQueryRepository = shareQueryRepository;
    }

    public async Task<Result<List<ShareDTO>>> Handle(GetShareListQuery request, CancellationToken cancellationToken)
    {
        var sharesDto = await _shareQueryRepository.GetSharesList(request.limit, request.offSet);

        if (!sharesDto.Any())
            return sharesDto;

        foreach (var share in sharesDto)
        {
            share.ShareType = (share.ShareType != null) ? Enum.Parse(typeof(ShareTypes), share.ShareType!).ToString() : "";
        }
        return sharesDto;
    }
}
