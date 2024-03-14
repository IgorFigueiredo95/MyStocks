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

public class GetShareByIdQueryHandler:IRequestHandler<GetShareByIdQuery, Result<ShareDTO>>
{
    private readonly IShareQueryRepository _ShareQueryRepository;

    public GetShareByIdQueryHandler(IShareQueryRepository shareQueryRepository)
    {
        _ShareQueryRepository = shareQueryRepository;
    }

    public async Task<Result<ShareDTO>> Handle(GetShareByIdQuery request, CancellationToken cancellationToken)
    {
        var share =  await _ShareQueryRepository.GetShareById(request.Id);

        if (share is null)
            return Error.Create("SHARE_NOT_FOUND", $"Share with id '{request.Id}'Not Found");

        share.ShareType = (share.ShareType != null) ? Enum.Parse(typeof(ShareTypes), share.ShareType!).ToString(): "";

        return share;
    }
}
