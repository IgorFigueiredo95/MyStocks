using FluentValidation;
using MyStocks.Application.Shares.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Validations;

public class GetShareDetailListByCodeQueryValidation:AbstractValidator<GetShareDetailListByCodeQuery>
{
    public GetShareDetailListByCodeQueryValidation()
    {
        RuleFor(q=> q.Code)
            .NotEmpty()
            .WithMessage("A ShareDetail code must be informed.");

        RuleFor(q => q.Limit)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Limit cannot be less than 0.");


        RuleFor(q=> q.OffSet)
            .GreaterThanOrEqualTo(0)
            .WithMessage("OffSet cannot be less than 0.");

    }
}
