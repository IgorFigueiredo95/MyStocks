using FluentValidation;
using MediatR;
using MyStocks.Application.Currencies;
using MyStocks.Domain.Common;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Currencies;
using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.CurrenciesTypes;

public class CreateCurrencyTypesCommandHandler: IRequestHandler<CreateCurrencyTypeCommand, Result<Guid>>
{
    private readonly ICurrencyTypesRepository _currencyTypesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateCurrencyTypeCommand> _validator;


    public CreateCurrencyTypesCommandHandler(
        ICurrencyTypesRepository currencyTypesRepository, 
        IUnitOfWork unitOfWork, 
        IValidator<CreateCurrencyTypeCommand> validator)
    {
        _currencyTypesRepository = currencyTypesRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(CreateCurrencyTypeCommand request, CancellationToken cancellationToken)
    {

        var  resultValidation =  await _validator.ValidateAsync(request, cancellationToken);

        if (!resultValidation.IsValid)
        {
            var errors = new List<Error>();
            resultValidation.Errors.ForEach(errorResult => errors.Add(Error.Create("INPUT_VALIDATION_ERROR", errorResult.ErrorMessage)));
            return errors;
        }
        var IsUnique = await _currencyTypesRepository.CodeIsUniqueAsync(request.Code);

        if (!IsUnique)
            return Error.Create("CURRENCY_CODE_CONFLICT", $"The Code '{request.Code}' is already in use");

        var CurrencyType = CurrencyTypes.Create(request.Code, request.CurrencyCode, request.Name);

        await _currencyTypesRepository.AddAsync(CurrencyType);

        await _unitOfWork.CommitAsync();

        return CurrencyType.Id;

        
    }
}

