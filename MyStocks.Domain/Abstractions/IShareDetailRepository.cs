using MyStocks.Domain.Currencies;
using MyStocks.Domain.Shares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Abstractions;

public interface IShareDetailRepository
{
    public Task<Guid> AddAsync(ShareDetail share);

    public void Update(ShareDetail share);

    public void Remove(ShareDetail share);

    public Task<List<ShareDetail>> GetShareDetailByPagination(Guid ShareId, int Limit, int offSet);
}
