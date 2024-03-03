using MyStocks.Domain.Currencies;
using MyStocks.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Commands.DeleteShareDetail;

public record DeleteShareDetailResponseDTO(
    string Message,
    UpdatedShareResponse Share);

public record UpdatedShareResponse(
    string ShareCode,
    string ShareType,
    decimal TotalValueInvested,
    decimal TotalShares,
    decimal AveragePrice);