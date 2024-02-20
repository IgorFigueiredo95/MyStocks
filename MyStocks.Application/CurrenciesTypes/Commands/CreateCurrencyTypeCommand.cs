using MediatR;
using MyStocks.Domain.Common;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Currencies
{
    public record CreateCurrencyTypeCommand(
            string Code,
            string CurrencyCode,
            string Name
        ) : IRequest<Result<Guid>>;
}
