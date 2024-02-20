using FluentValidation;
using MyStocks.Domain;
using MyStocks.Domain.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.CurrenciesTypes;

public class UpdateCurrencyTypesCommandValidation: AbstractValidator<UpdateCurrencyTypeCommand>
{
    public UpdateCurrencyTypesCommandValidation()
    {

        RuleFor(c => c.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty.");

        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(Constants.MAX_CURRENCYNAME_LENGTH)
            .WithMessage($"Name cannot be empty or more than {Constants.MAX_CURRENCYNAME_LENGTH} characters.");
    }
}
