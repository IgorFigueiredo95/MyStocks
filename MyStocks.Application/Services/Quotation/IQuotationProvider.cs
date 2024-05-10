using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Services.Quotation;

public interface IQuotationProvider
{
    public Task<QuotationResponse?> GetQuotationDataAsync(string Sharecode, CancellationToken cancellationToken);
}
