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
public class ShareDetailListDTO
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ShareType { get; set; }
    public string? CurrencyCode { get; set; }
    public decimal? TotalValueInvested { get; set; }
    public decimal? TotalShares { get; set; }
    public decimal? AveragePrice { get; set; }
    public List<ShareDetailDTO>? SharesDetails { get; set; } = new List<ShareDetailDTO>();
}
    public class ShareDetailDTO 
{
    public Guid? ShareDetailId { get; set; }
    public string? OperationTypeCode { get; set; }
    public string? Note { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? Price { get; set; }
    public DateTime? CreatedAt { get; set; }
}
