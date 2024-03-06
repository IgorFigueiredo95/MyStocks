using FluentValidation.Results;
using MyStocks.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Common;

public static class FluentValidationExtensions
{
    public static List<Error> ReturnListErrors(this ValidationResult validationResult, string? ErrorType = "INPUT_VALIDATION_ERROR")
    {
        var errors = new List<Error>();
        validationResult.Errors.ForEach(errorResult => errors.Add(Error.Create(ErrorType, errorResult.ErrorMessage)));

        return errors;
    }
}
