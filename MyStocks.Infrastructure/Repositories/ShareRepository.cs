using Microsoft.EntityFrameworkCore;
using MyStocks.Domain.Abstractions;
using MyStocks.Domain.Shares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Infrastructure.Repositories;
public class ShareRepository :IShareRepository
{
    private readonly ApplicationDbContext _context;

    public ShareRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> AddAsync(Share share)
    {
        await _context.Shares.AddAsync(share);
        return share.Id;
    }

    public async Task<bool> CodeIsUniqueAsync(string code)
    {
        var exist = await _context.Shares.FirstOrDefaultAsync(share => share.Code == code) is Share;
        return !exist;
    }

    public async Task<Share?> GetByCodeAsync(string code)
    {
        //todo: melhorar todos os includes referentes ao currency type
        return await _context.Shares
            .Include(s => s.TotalValueInvested.CurrencyType)
            .Include(s => s.SharesDetails)
            .FirstOrDefaultAsync(s => s.Code == code);

    }

    public async Task<Share?> GetByIdAsync(Guid id)
    {
        return await _context.Shares
            .Include(S => S.TotalValueInvested.CurrencyType)
            .Include(s => s.SharesDetails)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public void Remove(Share share)
    {
        _context.Shares.Remove(share);
    }

    public void Update(Share share)
    {
        _context.Shares.Entry(share).State = EntityState.Modified;
    }
}
