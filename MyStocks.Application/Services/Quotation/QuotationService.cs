using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Services.Quotation;

public class QuotationService: IQuotationService
{
    private readonly IQuotationProvider _provider;
    public QuotationService(IQuotationProvider provider)
    {
        _provider = provider;
    }
    public async Task<decimal?> GetShareCurrentPrice(string stockCode, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _provider.GetQuotationDataAsync(stockCode, cancellationToken);

            return result.results.FirstOrDefault().regularMarketPrice;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
