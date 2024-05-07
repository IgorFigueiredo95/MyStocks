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

public record VariationResult(
    decimal CurrentPrice,
    decimal VariatiationPercentage,
    decimal VariatiationResult
);
    

