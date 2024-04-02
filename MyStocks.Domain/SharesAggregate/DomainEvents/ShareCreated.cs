using MyStocks.Domain.Common.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.SharesAggregate.DomainEvents;

public record ShareCreated(Guid Shareid):IdomainEvent;
