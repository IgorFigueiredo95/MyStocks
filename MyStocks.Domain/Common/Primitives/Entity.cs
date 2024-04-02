using MyStocks.Domain.Common.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Primitives;

public abstract class Entity
{
    private List<IdomainEvent> _RaisedEvents;
    public IReadOnlyCollection<IdomainEvent> RaisedEvents { get => _RaisedEvents; }

    public Guid Id { get; private set; }
    public Entity(Guid id)
    {
       Id = id;
        _RaisedEvents = new List<IdomainEvent>();
    }

    public void AddDomainEvent(IdomainEvent domainEvent)
    {
        _RaisedEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(IdomainEvent domainEvent)
    {
        _RaisedEvents?.Remove(domainEvent);
    }
}
