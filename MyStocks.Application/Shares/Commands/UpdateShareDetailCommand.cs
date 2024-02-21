using MediatR;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Currencies;
using MyStocks.Domain.Enums;
using MyStocks.Domain.Shares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Commands;

public record UpdateShareDetailCommand(
    Guid ShareDetailId,
    string? Note,
    decimal? Quantity,
    decimal? Price
    ):IRequest<Result>;