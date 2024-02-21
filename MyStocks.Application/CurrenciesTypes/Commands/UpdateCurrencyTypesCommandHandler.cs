using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using MyStocks.Application.Common;
using MyStocks.Domain.Common;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Currencies;
using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.CurrenciesTypes;
public class UpdateCurrencyTypesCommandHandler : IRequestHandler<UpdateCurrencyTypeCommand, Result>
{
    private readonly ICurrencyTypesRepository _currencyTypesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateCurrencyTypeCommand> _validator;

    public UpdateCurrencyTypesCommandHandler(ICurrencyTypesRepository currencyTypesRepository, IUnitOfWork unitOfWork, IValidator<UpdateCurrencyTypeCommand> validator)
    {
        _currencyTypesRepository = currencyTypesRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result> Handle(UpdateCurrencyTypeCommand request, CancellationToken cancellationToken)
    {
        var resultValidation = _validator.Validate(request);

        if (!resultValidation.IsValid)
            return resultValidation.ReturnListErrors();


        var currencyType = await _currencyTypesRepository.GetByIdAsync(request.Id);

        if (currencyType is null)
            return Error.Create("CURRENCY_TYPE_NOT_FOUND", "CurrencyType not found.");

        currencyType.UpdateName(request.Name);

        _currencyTypesRepository.Update(currencyType);

        await _unitOfWork.CommitAsync();

        return true;

    }
}
