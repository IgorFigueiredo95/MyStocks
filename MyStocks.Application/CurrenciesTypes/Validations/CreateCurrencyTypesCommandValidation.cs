using FluentValidation;
using MyStocks.Application.Currencies;
using MyStocks.Domain;
using MyStocks.Domain.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.CurrenciesTypes
{
    public class CreateCurrencyTypesCommandValidation: AbstractValidator<CreateCurrencyTypeCommand>
    {
        public CreateCurrencyTypesCommandValidation()
        {

            RuleFor(r => r.Code)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(Constants.MAX_CODE_LENGTH)
                .WithMessage($"Code cannot be empty and must be between 3 and {Constants.MAX_CODE_LENGTH} characters.");

            RuleFor(r => r.Name)
                .NotEmpty()
                .MaximumLength(Constants.MAX_CURRENCYNAME_LENGTH)
                .WithMessage($"Name cannot be empty or more than {Constants.MAX_CURRENCYNAME_LENGTH} characters.");

            RuleFor(r => r.CurrencyCode)
                .NotEmpty()
                .Length(Constants.MAX_CURRENCYCODE_LENGTH)
                .WithMessage("Currency code cannot be empty and must have 3 characters.");


        }
    }
}
