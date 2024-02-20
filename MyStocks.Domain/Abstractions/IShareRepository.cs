using MyStocks.Domain.Currencies;
using MyStocks.Domain.Shares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Abstractions
{
    public interface IShareRepository
    {
        public Task<Guid> AddAsync(Share share);

        public void Update(Share share);

        public void Remove(Share share);
        public Task<Share?> GetByIdAsync(Guid id);

        public Task<Share?> GetByCodeAsync(string code);

        public Task<bool> CodeIsUniqueAsync(string code);
    }
}
