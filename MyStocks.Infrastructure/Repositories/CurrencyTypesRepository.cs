using Microsoft.EntityFrameworkCore;
using MyStocks.Domain.Currencies;
using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Infrastructure.Currencies
{
    public class CurrencyTypesRepository : ICurrencyTypesRepository
    {
        private readonly ApplicationDbContext _context;

        public CurrencyTypesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Remove(CurrencyTypes currencyTypes)
        {
            _context.Remove(currencyTypes);
        }

        public async Task<CurrencyTypes?> GetByCodeAsync(string code)
        {
            return await _context.CurrencyTypes.FirstOrDefaultAsync(c => c.Code == code.Trim());
        }

        public async Task<CurrencyTypes?> GetByIdAsync(Guid id)
        {
            return await _context.CurrencyTypes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Guid> AddAsync(CurrencyTypes currencyTypes)
        {
            await _context.CurrencyTypes.AddAsync(currencyTypes);

            return  currencyTypes.Id;
        }

        public void Update(CurrencyTypes currencyTypes)
        {
            _context.Entry(currencyTypes).State = EntityState.Modified;
        }

        public async Task<bool> CodeIsUniqueAsync(string code)
        {
            var exist = await _context.CurrencyTypes.FirstOrDefaultAsync(c=> c.Code == code.Trim()) is CurrencyTypes;

            return !exist;
        }

        public async Task<bool> CurrencyCodeIsUniqueAsync(string currencyCode)
        {
            var exist = await _context.CurrencyTypes.FirstOrDefaultAsync(c => c.CurrencyCode == currencyCode.Trim().ToUpper()) is CurrencyTypes;

            return !exist;
        }

        public async Task<CurrencyTypes?> GetDefaultCurrencyType()
        {
             var CurrencyType = await _context.CurrencyTypes.FirstOrDefaultAsync(c => c.IsDefault == true);

            if(CurrencyType is null)
                throw new ValueNotFoundException("No default currency set.", new InvalidOperationException());

            return CurrencyType;
        }
    }
}
