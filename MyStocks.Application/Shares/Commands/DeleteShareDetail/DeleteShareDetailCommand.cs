using MediatR;
using MyStocks.Domain.Common.ResultObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Commands.DeleteShareDetail;

public record DeleteShareDetailCommand(Guid ShareDetailId):IRequest<Result<DeleteShareDetailResponseDTO>>;
