using MyStocks.Domain.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.PortfolioAggregate;

public interface IPortfolioRepository
{
    public Task<Guid> AddAsync(Portfolio portfolio);

    public void Update(Portfolio portfolio);

    public void Remove(Guid id);
    public Task<Portfolio> GetByIdAsync(Guid id);

    public Task<List<Portfolio>> GetAllAsync();
}

