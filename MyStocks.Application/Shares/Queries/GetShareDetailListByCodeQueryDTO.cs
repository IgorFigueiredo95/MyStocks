using MyStocks.Domain.Currencies;
using MyStocks.Domain.Enums;
using MyStocks.Domain.Shares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Queries;
//Experimento de uma forma de DTO sem pesquisar como é feito normalmente 
//TODO: pesquisar como realizar um DTO correto
public class GetShareDetailListByCodeQueryDTO
{
    public GetShareDetailListByCodeQueryDTO(
        string? code,
        string? name,
        string? description,
        string? shareTypeCode,
        decimal? totalValueInvested,
        string? currencyCode,
        decimal? totalShares,
        decimal? averagePrice,
        List<GetShareDetailListDTO> sharesDetails)
    {
        Code = code;
        Name = name;
        Description = description;
        ShareTypeCode = shareTypeCode;
        TotalValueInvested = totalValueInvested;
        CurrencyCode = currencyCode;
        TotalShares = totalShares;
        AveragePrice = averagePrice;
        SharesDetails = sharesDetails;
    }

    public string? Code { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? ShareTypeCode { get; init; }
    public decimal? TotalValueInvested { get; init; }
    public string? CurrencyCode { get; init; }
    public decimal? TotalShares { get; init; }
    public decimal? AveragePrice { get; init; }
    public List<GetShareDetailListDTO> SharesDetails { get; init; }

}
    public class GetShareDetailListDTO 
{
    public GetShareDetailListDTO(
        Guid? shareDetailId,
        string? note,
        decimal? quantity,
        decimal? price,
        string? currencyCode,
    string? operationTypeCode)
    {
        ShareDetailId = shareDetailId;
        Note = note;
        Quantity = quantity;
        Price = price;
        CurrencyCode = currencyCode;
        OperationTypeCode = operationTypeCode;
    }
    [JsonPropertyName("id")]
    public Guid? ShareDetailId { get; set; }
    public string? Note { get; init; }
    public decimal? Quantity { get; init; }
    public decimal? Price { get; init; }
    public string? CurrencyCode { get; set; }
    public string? OperationTypeCode { get; init; }

}
