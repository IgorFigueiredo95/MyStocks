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
using System.Security.Claims;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Queries;

public class GetShareByCodeQueryHandler : IRequestHandler<GetShareByCodeQuery, Result<ShareDTO>>
{
    private readonly IShareQueryRepository _ShareQueryRepository;
    private readonly IPrincipal _principal;

    public GetShareByCodeQueryHandler(IShareQueryRepository shareQueryRepository, IPrincipal principal)
    {
        _ShareQueryRepository = shareQueryRepository;
        _principal = principal;
    }

    public async Task<Result<ShareDTO>> Handle(GetShareByCodeQuery request, CancellationToken cancellationToken)
    {

        var share =  await _ShareQueryRepository.GetShareByCode(Guid.Parse(_principal.Identity.Name), request.Code);

        if (share is null)
            return Error.Create("SHARE_NOT_FOUND", $"Share with code '{request.Code}'Not Found");

        share.ShareType = (share.ShareType != null) ? Enum.Parse(typeof(ShareTypes), share.ShareType!).ToString(): "";

        return share;
    }
}
