using MediatR;
using MyStocks.Domain.Abstractions;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Enums;
using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Commands;

public class UpdateShareCommandHandler : IRequestHandler<UpdateShareCommand, Result>
{
    private readonly IShareRepository _shareRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateShareCommandHandler(
        IShareRepository shareRepository,
        IUnitOfWork unitOfWork)
    {
        _shareRepository = shareRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateShareCommand request, CancellationToken cancellationToken)
    {
        var share = await _shareRepository.GetByIdAsync(request.Id);

        if (share is null) 
            throw new ValueNotFoundException(nameof(share));

        share.Update(request.Name, request.Description, Enum.Parse<ShareTypes>(request.shareTypeCode));

        _shareRepository.Update(share);
        await _unitOfWork.CommitAsync();

        return true;
    }
}
