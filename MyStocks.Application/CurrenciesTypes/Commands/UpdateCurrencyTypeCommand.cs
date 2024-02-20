using MediatR;
using MyStocks.Domain.Common;
using MyStocks.Domain.Common.ResultObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.CurrenciesTypes;   
public record UpdateCurrencyTypeCommand(
        Guid Id,
        string Name
    ) : IRequest<Result>;


