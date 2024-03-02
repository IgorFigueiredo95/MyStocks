using MyStocks.Domain.Primitives;
using System;
using MyStocks.Domain.SharesAggregate.ValueObjects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.PortfolioAggregate.ValueObjects;

public class AssociatedShares : ValueObject
{
    public ShareId ShareId { get; private set; }

    public Guid PortfolioId { get; private set; }

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public override List<object> GetAtomicValues()
    {
        return new List<object> { ShareId };
    }


    private AssociatedShares() { }

    public AssociatedShares(ShareId shareId)
    {
        ShareId = shareId;
    }
    public static AssociatedShares Create(ShareId shareId)
    {
        return new AssociatedShares( shareId);
    }
}
