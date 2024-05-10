using MyStocks.Application.Queries;
using MyStocks.Application.Shares.Queries;
using MyStocks.Domain.SharesAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares;

public interface IShareQueryRepository
{
    public Task<ShareResponse?> GetShareByCode(Guid OwnerId, string code);

    public Task<List<ShareResponse?>> GetSharesList(Guid OwnerId, int? Limit = 15, int? offset = 0);

    public Task<ShareDetailListDTO?> GetShareDetailListByCode(Guid OwnerId, string Code, int? Limit = 15, int? offset = 0);

    
}
