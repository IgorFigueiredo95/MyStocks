using MediatR;
using MyStocks.Application.Queries;
using MyStocks.Domain.Abstractions;
using MyStocks.Domain.Shares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Queries;

public class GetShareByIdQueryHandler:IRequestHandler<GetShareByIdQuery,Share>
{
    private readonly IShareRepository _shareRepository;

    public GetShareByIdQueryHandler(IShareRepository shareRepository)
    {
        _shareRepository = shareRepository;
    }

    public async Task<Share> Handle(GetShareByIdQuery request, CancellationToken cancellationToken)
    {
        var share =  await _shareRepository.GetByIdAsync(request.Id);

        if(share is null)
            throw new KeyNotFoundException(nameof(share));

        return share;
    }
}
