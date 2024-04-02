using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Common.Abstractions;

public interface IHasOwner
{
    public Guid OwnerId { get; }
}
