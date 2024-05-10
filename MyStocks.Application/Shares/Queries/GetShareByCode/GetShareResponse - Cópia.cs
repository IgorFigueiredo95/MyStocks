//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Text.Json.Serialization;
//using System.Threading.Tasks;
//using System.Xml.Linq;

//namespace MyStocks.Application.Shares.Queries;

//public class GetShareResponse
//{
//    public GetShareResponse() { }
//    public GetShareResponse(
//        string? code,
//        string? name,
//        string? description,
//        string? shareType,
//        string? currencyType,
//        decimal? totalValueInvested,
//        decimal? totalShares,
//        decimal? averagePrice,
//        QuotationReponse? quotation)
//    {
//        Code = code;
//        Name = name;
//        Description = description;
//        ShareType = shareType;
//        CurrencyType = currencyType;
//        TotalValueInvested = totalValueInvested;
//        TotalShares = totalShares;
//        AveragePrice = averagePrice;
//        Quotation = quotation;
//    }

//    public string? Code { get; set; }
//    public string? Name { get; set; }
//    public string? Description { get; set; }
//    public string? ShareType { get; set; }
//    public string? CurrencyType { get; set; }
//    public decimal? TotalValueInvested { get; set; }
//    public decimal? TotalShares { get; set; }
//    public decimal? AveragePrice { get; set; }
//    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
//    public QuotationReponse? Quotation { get; set; }


//}
//public class QuotationReponse
//{
//    public decimal? VariatiationPercentage { get; set; }
//    public decimal? VariatiationResult { get; set; }

//    public QuotationReponse(
//        decimal? variatiationPercentage,
//        decimal? variatiationResult)
//    {
//        VariatiationPercentage = variatiationPercentage;
//        VariatiationResult = variatiationResult;
//    }
//}
    

