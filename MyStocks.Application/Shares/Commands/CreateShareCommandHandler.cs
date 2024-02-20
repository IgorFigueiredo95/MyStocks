using FluentValidation;
using MediatR;
using MyStocks.Domain.Abstractions;
using MyStocks.Domain.Common;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Currencies;
using MyStocks.Domain.Enums;
using MyStocks.Domain.Shares;
using System.Data;

namespace MyStocks.Application.Shares.Commands
{
    public class CreateShareCommandHandler : IRequestHandler<CreateShareCommand,Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShareRepository _shareRepository;
        private readonly ICurrencyTypesRepository _currencyTypesRepository;
        private readonly IValidator<CreateShareCommand> _validator;

        public CreateShareCommandHandler(
            IUnitOfWork unitOfWork,
            IShareRepository shareRepository,
            IValidator<CreateShareCommand> validator,
            ICurrencyTypesRepository currencyTypesRepository)
        {
            _unitOfWork = unitOfWork;
            _shareRepository = shareRepository;
            _validator = validator;
            _currencyTypesRepository = currencyTypesRepository;
        }

        public async Task<Result<Guid>> Handle(CreateShareCommand request, CancellationToken cancellationToken)
        {
            var validationResult =  _validator.Validate(request);

            if (!validationResult.IsValid) 
            {
                var error = new List<Error>();
                validationResult.Errors.ForEach(ErrorResult => error.Add(Error.Create("INPUT_VALIDATION_ERROR", ErrorResult.ErrorMessage)));
                return error;
            }

            var exist = await _shareRepository.CodeIsUniqueAsync(request.code);
            if (!exist)
                return Error.Create("SHARE_CODE_CONFLICT", $"Code '{request.code}' is already in use.");


            CurrencyTypes? currencyType;
            if (request.currencyTypeCode != string.Empty)
                currencyType = await _currencyTypesRepository.GetByCodeAsync(request.currencyTypeCode!);
            else
                currencyType = await _currencyTypesRepository.GetDefaultCurrencyType();

            if (currencyType is null)
                return Error.Create("CURRENCYTYPE_NOT_FOUND", $"An currency type with Code {request.currencyTypeCode} was not found " +
                    "and there is not defalt currencyType set");

            var shareTypeEnum = Enum.Parse<ShareTypes>(request.shareTypeCode);

            var share = Share.Create(request.code, request.name, request.description, shareTypeEnum, currencyType);

            await _shareRepository.AddAsync(share);
            await _unitOfWork.CommitAsync();

            return share.Id;

        }
    }
}
