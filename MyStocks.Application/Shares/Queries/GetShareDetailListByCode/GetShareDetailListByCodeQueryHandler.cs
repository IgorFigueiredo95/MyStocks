using FluentValidation;
using MediatR;
using MyStocks.Application.Common;
using MyStocks.Domain.Abstractions;
using MyStocks.Domain.Common;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Enums;
using MyStocks.Domain.Shares;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace MyStocks.Application.Shares.Queries;

public class GetShareDetailListByCodeQueryHandler : IRequestHandler<GetShareDetailListByCodeQuery, Result<ShareDetailListDTO>>
{
    private readonly IShareQueryRepository _shareQueryRepository;
    private readonly IValidator<GetShareDetailListByCodeQuery> _validator;

    public GetShareDetailListByCodeQueryHandler(
        IShareQueryRepository shareQueryRepository,
        IValidator<GetShareDetailListByCodeQuery> validator)
    {
        _shareQueryRepository = shareQueryRepository;
        _validator = validator;
    }
    public async Task<Result<ShareDetailListDTO>> Handle(GetShareDetailListByCodeQuery request, CancellationToken cancellationToken)
    {
        var resultValidation = _validator.Validate(request);

        if (!resultValidation.IsValid)
            return resultValidation.ReturnListErrors();

        var resultValue = await _shareQueryRepository.GetShareDetailListByCode(request.Code,request.Limit,request.OffSet);

        if (resultValue is null)
            return Error.Create("SHARE_NOT_FOUND", $"Share with code '{request.Code}' was not found.");

        Enum.TryParse(resultValue.ShareType, out ShareTypes shareType);
        resultValue.ShareType = shareType.ToString() ?? resultValue.ShareType;

        foreach(var shareDetail in resultValue.SharesDetails)
        {
            Enum.TryParse(shareDetail.OperationTypeCode, out OperationType operationType);
            shareDetail.OperationTypeCode = operationType.ToString() ?? shareDetail.OperationTypeCode;
        }

        return resultValue;
    }
}
