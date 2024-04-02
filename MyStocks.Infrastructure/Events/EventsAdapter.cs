using MediatR;
using MyStocks.Application.Common;
using MyStocks.Domain.Common.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Infrastructure.Events;

public static class EventsAdapter
{
    public static INotification TranslateDomainEventToNotification(IdomainEvent domainEvent)
    {
        //retorna o tipo de domain event.
        var domainType = domainEvent.GetType();

        //Cria uma instância do Event<DomainType> (cujo já implementa a interface Inotification)
        //adiciona o domainEvent recebido para a propriedade da classe
        //faz o cast para a Inotification para retornarmos como resposta ao Inotification
        var eventOfDomainEvent = (INotification)Activator
            .CreateInstance(typeof(Event<>)
                .MakeGenericType(domainType),
                domainEvent)!;

        return eventOfDomainEvent;
    }
}
