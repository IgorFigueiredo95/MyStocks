using MyStocks.Domain.Currencies;
using MyStocks.Domain.Enums;
using MyStocks.Domain.Shares;
using MyStocks.Domain.SharesAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Contracts.Shares;

public record GetShareByIdResponse(
    string Code,
    string Name,
    string? Description,
    ShareTypes ShareType,
    Currency TotalValueInvested,
    decimal TotalShares,
    Currency AveragePrice,
    List<ShareDetail> SharesDetails,
    DateTime CreatedAt,
    DateTime UpdatedAt);
