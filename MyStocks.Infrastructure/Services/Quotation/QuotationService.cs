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

    private string Quotation = "api/quote";

    public QuotationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> GetQuotationDataAsync(string Sharecode)
    {
        var response = await _httpClient.GetFromJsonAsync<QuotationResponse>($"{Quotation}/{Sharecode}");
        return response.results.FirstOrDefault().regularMarketPrice.Value;
    }
}
