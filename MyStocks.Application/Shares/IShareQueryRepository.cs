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
    public Task<ShareDTO?> GetShareById(Guid id);
}
