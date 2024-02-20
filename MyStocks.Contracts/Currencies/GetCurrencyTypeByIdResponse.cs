using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Contracts.Currencies;

public record GetCurrencyTypeByIdResponse(
     Guid Id,
     string Code,
     string CurrencyCode,
     string Name
);

