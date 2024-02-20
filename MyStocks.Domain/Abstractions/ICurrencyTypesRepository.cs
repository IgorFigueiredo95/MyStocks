using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Currencies
{
    public interface ICurrencyTypesRepository
    {
        public Task<Guid> AddAsync(CurrencyTypes currencyTypes);

        public void Update(CurrencyTypes currencyTypes);

        public void Remove(CurrencyTypes currencyTypes);
        public Task<CurrencyTypes?> GetByIdAsync(Guid id);

        public Task<CurrencyTypes?> GetByCodeAsync(string code);

        public Task<bool> CodeIsUniqueAsync(string code);

        public Task<bool> CurrencyCodeIsUniqueAsync(string currencyCode);

        public Task<CurrencyTypes?> GetDefaultCurrencyType();
    }
}
