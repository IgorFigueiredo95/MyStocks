using MyStocks.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public UnitOfWork(ApplicationDbContext context)
    {

        _context = context;

    }
    public async  Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task RollbackAsync()
    {
      await _context.DisposeAsync();
    }
}
