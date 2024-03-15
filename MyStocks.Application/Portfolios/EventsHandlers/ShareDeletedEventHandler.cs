using MediatR;
using MyStocks.Application.Common;
using MyStocks.Domain.PortfolioAggregate;
using MyStocks.Domain.SharesAggregate.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Portfolios.EventsHandlers;

public class ShareDeletedEventHandler : INotificationHandler<Event<ShareDeleted>>
{
    private readonly IPortfolioRepository _portfolioRepository;
    public ShareDeletedEventHandler(IPortfolioRepository portfolioRepository)
    {
        _portfolioRepository = portfolioRepository;
    }
    public async Task Handle(Event<ShareDeleted> notification, CancellationToken cancellationToken)
    {
        var portfolios = await _portfolioRepository.ContainsShareIdAsync(notification.DomainEvent.ShareId);

        if (portfolios is null)
            return;

        foreach(var portfolio in portfolios)
        {
            portfolio.RemoveShare(notification.DomainEvent.ShareId);
        }
    }
}
