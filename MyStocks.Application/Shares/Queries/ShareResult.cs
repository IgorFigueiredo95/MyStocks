using MyStocks.Application.Quotation;
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

public record ShareResult(
    string code,
    string name,
    string? description,
    string shareType,
    string currencyType,
    decimal totalValueInvested,
    decimal totalShares,
    decimal averagePrice,
    VariationResult Quotation
    );