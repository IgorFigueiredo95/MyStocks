using MediatR;
using MyStocks.Application;
using MyStocks.Domain.Common.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IMediator _mediator;
    public UnitOfWork(ApplicationDbContext context, IMediator mediator)
    {

        _context = context;
        _mediator = mediator;
    }
    public void DispatchDomainEvents(IReadOnlyCollection<IdomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            var domEvent  = GetNotificationFromDomainEvent(domainEvent);
            _mediator.Publish(domEvent);
        }
    }
    private INotification GetNotificationFromDomainEvent(IdomainEvent domainEvent)
    {
        INotification notificationAbstraction = null;
       var notification =  Activator.CreateInstance(domainEvent.GetType(), notificationAbstraction);
        return (INotification)notification;
    }
    public async  Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task RollbackAsync()
    {
      await _context.DisposeAsync();
    }
}
