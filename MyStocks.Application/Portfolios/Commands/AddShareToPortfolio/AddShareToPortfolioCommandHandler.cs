using MediatR;
using MyStocks.Domain.Abstractions;
using MyStocks.Domain.Common;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.PortfolioAggregate;
using MyStocks.Domain.SharesAggregate.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Portfolios.Commands.AddShareToPortfolio;

public class AddShareToPortfolioCommandHandler : IRequestHandler<AddShareToPortfolioCommand, Result>
{
    private readonly IShareRepository _shareRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IUnitOfWork _unitOfWork;
    public AddShareToPortfolioCommandHandler(
        IShareRepository shareRepository,
        IPortfolioRepository portfolioRepository,
        IUnitOfWork unitOfWork)
    {
        _shareRepository = shareRepository;
        _portfolioRepository = portfolioRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(AddShareToPortfolioCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _portfolioRepository.GetByIdAsync(request.PortfolioId);

        if (portfolio is null)
            return Error.Create("PORTFOLIO_NOT_FOUND", $"Portfolio with id '{request.PortfolioId}' not found.");

        var share = await _shareRepository.GetByCodeAsync(request.ShareCode);

        if (share is null)
            return Error.Create("SHARE_NOT_FOUND", $"Share with code '{request.ShareCode}' not found.");

        try
        {
            portfolio.AddShare(ShareId.Create(share.Id));
            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            return Error.Create("INVALID_OPERATION", ex.Message);
        }

        return Result.ReturnSuccess();
    }
}
