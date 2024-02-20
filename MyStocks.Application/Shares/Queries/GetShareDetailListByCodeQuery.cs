using MediatR;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Shares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Queries;

public record GetShareDetailListByCodeQuery(string Code, int OffSet, int Limit):IRequest<Result<GetShareDetailListByCodeQueryDTO>>;
