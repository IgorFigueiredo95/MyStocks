using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Services.Quotation;

public record QuotationResponse(
    List<QuotationData> results,
    DateTime requestedAt,
    string took);

public record QuotationData(
    string? currency,
    decimal? twoHundredDayAverage,
    decimal? twoHundredDayAverageChange,
    decimal? twoHundredDayAverageChangePercent,
    decimal? marketCap,
    string? shortName,
    string? longName,
    decimal? regularMarketChange,
    decimal? regularMarketChangePercent,
    DateTime? regularMarketTime,
    decimal? regularMarketPrice,
    decimal? regularMarketDayHigh,
    string? regularMarketDayRange,
    decimal? regularMarketDayLow,
    decimal? regularMarketVolume,
    decimal? regularMarketPreviousClose,
    decimal? regularMarketOpen,
    double? averageDailyVolume3Month,
    double? averageDailyVolume10Day,
    decimal? fiftyTwoWeekLowChange,
    string? fiftyTwoWeekRange,
    decimal? fiftyTwoWeekHighChange,
    decimal? fiftyTwoWeekHighChangePercent,
    decimal? fiftyTwoWeekLow,
    decimal? fiftyTwoWeekHigh,
    string? symbol
   );