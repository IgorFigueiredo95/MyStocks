using MediatR;
using MyStocks.Application;
using MyStocks.Application.Common;
using MyStocks.Application.CurrenciesTypes.Queries;
using MyStocks.Domain.Common.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    public async Task DispatchDomainEventsAsync(IReadOnlyCollection<IdomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            
            try
            {
                var domEvent = INotificatonAdapter.TranslateDomainEventToNotification(domainEvent);
                await  _mediator.Publish(domEvent);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task RollbackAsync()
    {
      await _context.DisposeAsync();
    }
}