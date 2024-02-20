using MediatR;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Currencies;
using MyStocks.Domain.Enums;
using MyStocks.Domain.Shares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Commands;

public record CreateShareDetailCommand(
    string ShareCode,
    string? Note,
    decimal Quantity,
    decimal Price,
    string OperationTypeCode):IRequest<Result<Guid>>;