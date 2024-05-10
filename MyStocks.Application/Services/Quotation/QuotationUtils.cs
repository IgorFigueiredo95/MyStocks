using MyStocks.Domain.Common.ResultObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Quotation;

public static class QuotationUtils
{
    /// <summary>
    /// Return the diference as a decimal percentage between Value and baseValue.
    /// </summary>
    /// <param name="Value">Value wich will be compared</param>
    /// <param name="baseValue">Value that will be used as reference</param>
    /// <returns>The percentage variation between baseValue and Value</returns>
    public static decimal GetVariationPercentage(decimal Value, decimal baseValue)
    {
        if (baseValue == 0)
            throw new ArgumentException("BaseValue cannot bet 0.", nameof(baseValue));

        var result = ((Value / baseValue)-1) * 100;

        return Math.Round(result, 2);
    }

    /// <summary>
    /// Return the the percentage from baseValue.
    /// </summary>
    /// <param name="percentage">Percentage to be returned</param>
    /// <param name="averagePrice">Average price of the stock</param>
    /// <param name="totalShares">Total shares </param>
    /// <returns>Corresponding value of the percentage extracted from baseValue.</returns>
    public static decimal GetCorrespondingValue(decimal percentage, decimal averagePrice, decimal totalShares)
    {
        var result = (percentage / 100 * averagePrice) * totalShares;
        Math.Round(result, 2);

        return Math.Round(result, 2);
    }
}
