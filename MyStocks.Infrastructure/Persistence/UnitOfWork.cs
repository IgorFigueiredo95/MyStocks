using MediatR;
using MyStocks.Application;
using MyStocks.Application.Common;
using MyStocks.Application.CurrenciesTypes.Queries;
using MyStocks.Domain.Common.Abstractions;
using MyStocks.Domain.Common.Primitives;
using MyStocks.Domain.Primitives;
using MyStocks.Domain.Users;
using MyStocks.Infrastructure.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly IPrincipal _principal;
    public UnitOfWork(ApplicationDbContext context, IMediator mediator, IPrincipal principal)
    {

        _context = context;
        _mediator = mediator;
        _principal = principal;
    }

    public async Task CommitAsync()
    {
        SetEntityOwners(_context);
        await DispatchDomainEventsAsync(_context);

        await _context.SaveChangesAsync();
    }

    public async Task RollbackAsync()
    {
        await _context.DisposeAsync();
    }

    private void SetEntityOwners(ApplicationDbContext context)
    {
        foreach (var entry in context.ChangeTracker.Entries<IHasOwner>())
        {
            if (entry.Property(x => x.OwnerId).CurrentValue == Guid.Empty)
            {

                if (_principal.Identity.Name is null)
                    throw new Exception();

                var userId = Guid.Parse(_principal.Identity.Name);
                entry.Property(x => x.OwnerId).CurrentValue = userId;
            }
        }
    }

    private async Task DispatchDomainEventsAsync(ApplicationDbContext context)
    {
        var entitiesWithDomainEvent = context.ChangeTracker.Entries<Entity>()
            .Where(entitiesEntry => entitiesEntry
                .Property(x => x.RaisedEvents).CurrentValue.Count >= 1);

        foreach (var entity in entitiesWithDomainEvent)
        {
            var domainEvents = entity.Property(x => x.RaisedEvents).CurrentValue;
            foreach (var domainEvent in domainEvents)
            {

                try
                {
                    var domEvent = EventsAdapter.TranslateDomainEventToNotification(domainEvent);
                    await _mediator.Publish(domEvent);
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }
    }
}