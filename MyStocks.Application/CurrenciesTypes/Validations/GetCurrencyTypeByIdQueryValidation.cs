using FluentValidation;
using MyStocks.Application.CurrenciesTypes.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.CurrenciesTypes.Validations
{
    public class GetCurrencyTypeByIdQueryValidation:AbstractValidator<GetCurrencyTypeByIdQuery>
    {
        public GetCurrencyTypeByIdQueryValidation()
        {
            RuleFor(q => q.Id)
                .NotEmpty()
                .WithMessage("Id cannot be empty.");
        }
    }
}
