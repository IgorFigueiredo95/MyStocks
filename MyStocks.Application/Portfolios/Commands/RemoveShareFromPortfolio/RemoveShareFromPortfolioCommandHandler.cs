using MediatR;
using MyStocks.Domain.Abstractions;
using MyStocks.Domain.Common;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.PortfolioAggregate;
using MyStocks.Domain.SharesAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Portfolios.Commands.RemoveShareFromPortfolio;

public class RemoveShareFromPortfolioCommandHandler : IRequestHandler<RemoveShareFromPortfolioCommand, Result>
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IShareRepository _shareRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RemoveShareFromPortfolioCommandHandler(
        IPortfolioRepository portfolioRepository,
        IShareRepository shareRepository,
        IUnitOfWork unitOfWork)
    {
        _portfolioRepository = portfolioRepository;
        _shareRepository = shareRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(RemoveShareFromPortfolioCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _portfolioRepository.GetByIdAsync(request.PortfolioId);

        if (portfolio is null)
            return Error.Create("PORTFOLIO_NOT_FOUND", $"Portfolio with id '{request.PortfolioId}' not found.");

        var share = await _shareRepository.GetByCodeAsync(request.ShareCode);

        if (share is null)
            return Error.Create("SHARE_NOT_FOUND", $"Share with code '{request.ShareCode}' not found.");

        try
        {
            //TODO: não faz sentido criar um VO SHareId para o Share.Id 
            portfolio.RemoveShare(ShareId.Create(share.Id));
            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            return Error.Create("INVALID_OPERATION", ex.Message);
        }

        return Result.ReturnSuccess();

    }
}
