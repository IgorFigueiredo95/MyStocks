using MyStocks.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.PortfolioAggregate.ValueObjects;

public class AssociatedShares : ValueObject
{
    public Guid PortfolioId { get; private set; }
    public Guid SharedId { get; private set; }

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public override List<object> GetAtomicValues()
    {
        return new List<object> { PortfolioId, SharedId };
    }


    private AssociatedShares() { }

    public AssociatedShares(Guid portfolioId, Guid shareId)
    {
        PortfolioId = portfolioId;
        SharedId = shareId;
    }
    public static AssociatedShares Create(Guid portfolioId, Guid shareId)
    {
        return new AssociatedShares( portfolioId, shareId);
    }
}
