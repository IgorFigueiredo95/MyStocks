using MyStocks.Application.Services.Quotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Infrastructure.Services.Quotation;

public class QuotationService : IQuotationService
{
    private readonly HttpClient _httpClient;

    private const string Quotation = "api/quote";

    public QuotationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> GetQuotationValue(string Sharecode)
    {
        var response = await _httpClient.GetFromJsonAsync<QuotationData>($"{Quotation}/{Sharecode}");
        
        return response.regularMarketPrice.Value;
    }
}
