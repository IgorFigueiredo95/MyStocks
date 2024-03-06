using MyStocks.Domain.Common.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application
{
    public interface IUnitOfWork
    {
        public Task DispatchDomainEventsAsync(IReadOnlyCollection<IdomainEvent> domainEvents);
        public Task CommitAsync();

        public Task RollbackAsync();
    }
}
