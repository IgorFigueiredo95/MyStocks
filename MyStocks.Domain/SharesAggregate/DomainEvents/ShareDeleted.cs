using MyStocks.Domain.Common.Primitives;
using MyStocks.Domain.SharesAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.SharesAggregate.DomainEvents;

public class ShareDeleted : IdomainEvent
{
    public ShareId ShareId { get; }

    public ShareDeleted(ShareId shareId)
    {
        ShareId = shareId;
    }
}
