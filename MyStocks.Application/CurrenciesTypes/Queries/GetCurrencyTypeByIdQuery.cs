using MediatR;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.CurrenciesTypes.Queries;

public record GetCurrencyTypeByIdQuery(Guid Id) : IRequest<Result<CurrencyTypes>>;

