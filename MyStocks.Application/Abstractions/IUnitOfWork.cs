using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application
{
    public interface IUnitOfWork
    {
        public Task CommitAsync();

        public Task RollbackAsync();
    }
}
