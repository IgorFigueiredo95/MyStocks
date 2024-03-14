using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyStocks.Application.Shares.Queries;

public class ShareDTO
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ShareType { get; set; }
    public string? CurrencyType { get; set; }
    public decimal? TotalValueInvested { get; set; }
    public decimal? TotalShares { get; set; }
    public decimal? AveragePrice { get; }

}
