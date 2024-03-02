using FluentValidation;
using FluentValidation.Validators;
using MyStocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Portfolios.Commands;

public class CreatePortfolioCommandValidation: AbstractValidator<CreatePortfolioCommand> 
{
    public CreatePortfolioCommandValidation()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(Constants.MAX_PORTFOLIONAME_LENGTH)
            .WithMessage($"Portfolio name cannot be empty or bigger than {Constants.MAX_PORTFOLIONAME_LENGTH} characters.");

        RuleFor(c => c.Description)
            .MaximumLength(Constants.MAX_PORTFOLIDESCRIPTION_LENGTH)
            .WithMessage($"Portfolio description cannot be empty or bigger than {Constants.MAX_PORTFOLIDESCRIPTION_LENGTH} characters.");

    }
}
