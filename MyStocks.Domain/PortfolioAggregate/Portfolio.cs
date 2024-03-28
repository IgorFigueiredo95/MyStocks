using MyStocks.Domain.Common.Primitives;
using MyStocks.Domain.PortfolioAggregate.Exceptions;
using MyStocks.Domain.PortfolioAggregate.ValueObjects;
using MyStocks.Domain.Primitives;
using MyStocks.Domain.SharesAggregate.ValueObjects;
using MyStocks.Domain.SharesAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyStocks.Domain.PortfolioAggregate;

public class Portfolio : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string? Description { get; private set; }

    private readonly List<AssociatedShares> _ShareIds = new List<AssociatedShares>();
    public IReadOnlyCollection<AssociatedShares> ShareIds { get => _ShareIds; }
    public int SharesCount { get; private set; } = 0;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Guid OwnerId { get; private set; }



    public Portfolio() : base(Guid.NewGuid()) { }

    public Portfolio(string name, string description) : base(Guid.NewGuid()) 
    {
        Name = name;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }

    public static Portfolio Create(string name, string? description)
    {
        if (name is null || name.Length > Constants.MAX_PORTFOLIONAME_LENGTH)
            throw new InvalidPortifolioNameLengthException(nameof(name), Constants.MAX_PORTFOLIONAME_LENGTH);

        if (description is null || description.Length > Constants.MAX_PORTFOLIDESCRIPTION_LENGTH)
            throw new InvalidPortifolioDescriptionException(nameof(description), Constants.MAX_PORTFOLIDESCRIPTION_LENGTH);

        return new(name, description);
    }

    public Portfolio Update(string name, string? description = "")
    {
        if (name is null || name.Length > Constants.MAX_PORTFOLIONAME_LENGTH)
            throw new InvalidPortifolioNameLengthException(nameof(name), Constants.MAX_PORTFOLIONAME_LENGTH);

        if (description is null || description.Length > Constants.MAX_PORTFOLIDESCRIPTION_LENGTH)
            throw new InvalidPortifolioDescriptionException(nameof(description), Constants.MAX_PORTFOLIDESCRIPTION_LENGTH);

        Name = name ?? Name;
        Description = (description  == "" ? Description : description);
        UpdatedAt = DateTime.UtcNow;

        return this;
    }

    public void AddShare(ShareId shareId)
    {
        if (_ShareIds.Any(x => x.ShareId == shareId))
            throw new InvalidOperationException($"The ShareId '{shareId}' is already on portfolio.");

        var portfolioShare = AssociatedShares.Create(shareId);
        _ShareIds.Add(portfolioShare);

        UpdatedAt = DateTime.UtcNow;
        SharesCount++;
    }

    public void RemoveShare(ShareId shareId)
    {
        var portfolioShare = _ShareIds.Find(x => x.ShareId == shareId);
        if (portfolioShare is null)
            throw new InvalidOperationException($"The ShareId '{shareId}' is not on the portfolio.");

        _ShareIds.Remove(portfolioShare);

        UpdatedAt = DateTime.UtcNow;
        SharesCount--;
    }
    
}
