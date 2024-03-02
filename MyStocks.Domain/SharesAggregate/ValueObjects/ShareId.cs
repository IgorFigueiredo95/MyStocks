using MyStocks.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.SharesAggregate.ValueObjects;

public class ShareId : ValueObject
{
   public Guid Id { get; private set; }

    public override List<object> GetAtomicValues()
    {
        return new List<object>() {Id};
    }
    public ShareId(Guid id)
    {
        Id = id;
    }
    public static ShareId Create(Guid id)
    {
        return new ShareId(id);
    }

    public override string ToString()
    {
        return Id.ToString();
    }
}
