using MediatR;
using MyStocks.Domain.Common.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Common;
//Classe necessária para realizarmos a conversão do evento de IdomainEvent para Inotification
//Inotification é uma interface do mediatr e não queremos colocar dependencias a pacotes no nosso domain.
public class Event<TdomainEvent> : INotification
    where TdomainEvent : class, IdomainEvent
{
    public TdomainEvent DomainEvent { get; }

    public Event(TdomainEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }
}

   