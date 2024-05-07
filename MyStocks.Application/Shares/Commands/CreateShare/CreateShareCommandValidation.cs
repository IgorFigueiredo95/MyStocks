using FluentValidation;
using MyStocks.Application.Shares.Commands;
using MyStocks.Domain;
using MyStocks.Domain.Abstractions;
using MyStocks.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Validations;
public class CreateShareCommandValidation : AbstractValidator<CreateShareCommand>
{
    public CreateShareCommandValidation()
    {

        RuleFor(c => c.code)
            .NotEmpty()
            .MaximumLength(Constants.MAX_CODE_LENGTH)
            .WithMessage($"Code cannot be empty or bigger than {Constants.MAX_CODE_LENGTH} characters.");

        RuleFor(c => c.name)
            .NotEmpty()
            .MaximumLength(Constants.MAX_SHARENAME_LENGTH)
            .WithMessage($"Name cannot be empty or bigger than {Constants.MAX_CODE_LENGTH} characters.");

        RuleFor(c => c.description)
            .MaximumLength(Constants.MAX_SHAREDESCRIPTION_LENGTH)
            .WithMessage($"Descriptions cannot be  bigger than {Constants.MAX_SHAREDESCRIPTION_LENGTH} characters.");

        RuleFor(c => c.shareTypeCode)
            .NotNull()
            .IsEnumName(typeof(ShareTypes),false)
            .WithMessage("ShareType must be filled.");

    }
}
