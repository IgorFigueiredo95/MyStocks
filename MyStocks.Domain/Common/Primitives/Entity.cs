using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Primitives
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        public Entity(Guid id)
        {
           Id = id;
        }
    }
}
