using MyStocks.Domain.Currencies;
using MyStocks.Domain.Enums;
using MyStocks.Domain.SharesAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyStocks.Application.Shares.Queries;

public class ShareDTO
{
    public ShareDTO(){}
    public ShareDTO(
        string? code,
        string? name,
        string? description,
        int? shareType,
        string? currencyType,
        decimal? totalValueInvested,
        decimal? totalShares,
        decimal? averagePrice,
        DateTime? createdAt,
        DateTime? updatedAt)
    {
        Code = code;
        Name = name;
        Description = description;
        ShareType = shareType;
        CurrencyType = currencyType;
        TotalValueInvested = totalValueInvested;
        TotalShares = totalShares;
        AveragePrice = averagePrice;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public string? Code { get; }
    public string? Name { get; }
    public string? Description { get; }
    public int? ShareType { get; }
    public string? CurrencyType { get; }
    public decimal? TotalValueInvested { get; }
    public decimal? TotalShares { get; }
    public decimal? AveragePrice { get; }
    public DateTime? CreatedAt { get; }
    public DateTime? UpdatedAt { get; } 
}