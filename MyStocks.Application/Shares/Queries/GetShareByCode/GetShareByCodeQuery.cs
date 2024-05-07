using MediatR;
using MyStocks.Application.Shares.Queries;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Shares;
using MyStocks.Domain.SharesAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Queries;

public record GetShareByCodeQuery(string Code):IRequest<Result<GetShareResponse>>;
