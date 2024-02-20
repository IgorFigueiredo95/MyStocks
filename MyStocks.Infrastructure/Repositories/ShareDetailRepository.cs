using Microsoft.EntityFrameworkCore;
using MyStocks.Domain.Abstractions;
using MyStocks.Domain.Shares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Infrastructure.Repositories;
public class ShareDetailRepository :IShareDetailRepository
{
    private readonly ApplicationDbContext _context;

    public ShareDetailRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> AddAsync(ShareDetail share)
    {
        await _context.ShareDetails.AddAsync(share);
        return share.Id;
    }

    public async Task<ShareDetail?> GetByIdAsync(Guid id)
    {
        return await _context.ShareDetails
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public Task<List<ShareDetail>> GetShareDetailByPagination(Guid ShareId, int Limit, int offSet)
    {
        return _context.ShareDetails
            .Where(q => q.ShareId == ShareId)
            .Skip(offSet).Take(Limit)
            .ToListAsync();
    }

    public void Remove(ShareDetail share)
    {
        _context.ShareDetails.Remove(share);
    }

    public void Update(ShareDetail share)
    {
        _context.ShareDetails.Entry(share).State = EntityState.Modified;
    }
    
}
