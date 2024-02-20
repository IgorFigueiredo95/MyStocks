using FluentValidation;
using FluentValidation.Results;
using MediatR;
using MyStocks.Domain.Common;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Currencies;
using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.CurrenciesTypes.Queries
{
    internal class GetCurrencyTypeByIdQueryHandler : IRequestHandler<GetCurrencyTypeByIdQuery, Result<CurrencyTypes>>
    {
        private readonly ICurrencyTypesRepository _currencyTypesRepository;
        private readonly IValidator<GetCurrencyTypeByIdQuery> _validator;
        public GetCurrencyTypeByIdQueryHandler(ICurrencyTypesRepository currencyTypesRepository, IValidator<GetCurrencyTypeByIdQuery> validator)
        {
            _currencyTypesRepository = currencyTypesRepository;
            _validator = validator;
        }
        public async Task<Result<CurrencyTypes>> Handle(GetCurrencyTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var resultValidation  = _validator.Validate(request);

            if (!resultValidation.IsValid)
            {
                var error = new List<Error>();
                resultValidation.Errors.ForEach(errorResult => error.Add(Error.Create("input_validation_error", errorResult.ErrorMessage)));
            }
            
            var CurrencyType = await _currencyTypesRepository.GetByIdAsync(request.Id);

            if (CurrencyType is null)
                return Error.Create("CURRENCY_TYPE_NOT_FOUND", "CurrencyType not found.");

            return CurrencyType;
        }
    }
}
