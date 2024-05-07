using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MyStocks.Application.Services.Quotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Infrastructure.Services.Quotation;

public class QuotationProvider : IQuotationProvider
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<QuotationConfig> _quotationConfig;

    private const string Quotation = "api/quote";

    public QuotationProvider(
        HttpClient httpClient,
        IOptions<QuotationConfig> quotationConfig)
    {

        _httpClient = httpClient;
        _quotationConfig = quotationConfig;

        Configure(_httpClient, _quotationConfig);
    }

    public async Task<QuotationResponse?> GetQuotationDataAsync(string Sharecode, CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<QuotationResponse>($"{Quotation}/{Sharecode}", cancellationToken);
    }

    #region Configure
    private HttpClient Configure(HttpClient httpclient, IOptions<QuotationConfig> quotationConfig)
    {

        httpclient.BaseAddress = new Uri(quotationConfig.Value.BaseAddress.ToString());
        httpclient.DefaultRequestHeaders.Add("Authorization", "Bearer " + quotationConfig.Value.Token);
        httpclient.DefaultRequestHeaders.Add("Accept", "application/json; charset=utf-8");

        return httpclient;
    }
    #endregion
}
