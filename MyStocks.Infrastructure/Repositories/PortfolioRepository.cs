using Microsoft.EntityFrameworkCore;
using MyStocks.Domain.PortfolioAggregate;
using MyStocks.Domain.SharesAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Infrastructure.Repositories;

public class PortfolioRepository : IPortfolioRepository
{
    private readonly ApplicationDbContext _context;
    public PortfolioRepository( ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> AddAsync(Portfolio portfolio)
    {
        await _context.Portfolios.AddAsync(portfolio);

        return portfolio.Id;
    }

    public async Task<List<Portfolio>> GetAllAsync()
    {
        return await _context.Portfolios.ToListAsync();
    }

    public async Task<Portfolio?> GetByIdAsync(Guid id)
    {
        return await _context.Portfolios
            .Where(x => x.Id == id)
            .Include(x=>x.ShareIds)
            .FirstOrDefaultAsync();
    }

    public void Remove(Guid id)
    {
        var portfolio = GetByIdAsync(id);

        if (portfolio is not  null)
            _context.Remove(portfolio);
    }

    public void Update(Portfolio portfolio)
    {
        _context.Portfolios
            .Entry(portfolio).State = EntityState.Modified;
    }

    public async Task<List<Portfolio>?> ContainsShareIdAsync(ShareId shareId)
    {
        return await _context.Portfolios
            .Where(x => x.ShareIds
                              .Any(y => y.ShareId == shareId))
            .ToListAsync();
    }
}
