using MediatR;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Currencies;
using MyStocks.Domain.Enums;
using MyStocks.Domain.Primitives;
using MyStocks.Domain.Shares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Commands;
public record CreateShareCommand (
    string code,
    string name,
    string? description,
    string shareTypeCode,
    string? currencyTypeCode
    ) : IRequest<Result<Guid>>;
