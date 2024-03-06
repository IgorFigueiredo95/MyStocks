using MediatR;
using MyStocks.Domain.Common.ResultObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Commands.DeleteShare
{
    public record DeleteShareCommand(Guid ShareId):IRequest<Result>;
}
