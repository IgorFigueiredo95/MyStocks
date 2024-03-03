using MediatR;
using MyStocks.Application.Common;
using MyStocks.Domain.SharesAggregate.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Portfolios.EventsHandlers;

public class ShareDeletedEventHandler : INotificationHandler<Event<ShareDeletedDomainEvent>>
{
    public Task Handle(Event<ShareDeletedDomainEvent> notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
