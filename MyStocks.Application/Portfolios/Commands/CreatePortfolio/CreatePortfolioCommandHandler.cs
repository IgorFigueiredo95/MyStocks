using FluentValidation;
using MediatR;
using MyStocks.Application.Common;
using MyStocks.Domain.Common;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.PortfolioAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Portfolios.Commands;

public class CreatePortfolioCommandHandler : IRequestHandler<CreatePortfolioCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IValidator<CreatePortfolioCommand> _validator;

    public CreatePortfolioCommandHandler(
        IPortfolioRepository portfolioRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreatePortfolioCommand> validator)
    {
        _portfolioRepository = portfolioRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }
    public async Task<Result<Guid>> Handle(CreatePortfolioCommand request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
            return validationResult.ReturnListErrors();

        Portfolio portfolio;
        try
        {
            portfolio = Portfolio.Create(request.Name, request.Description);
        }
        catch (Exception ex)
        {
            return Error.Create("INVALID_OPERATION", ex.Message);
        }

       await  _portfolioRepository.AddAsync(portfolio);
       await _unitOfWork.CommitAsync();

        return portfolio.Id;
    }
}
