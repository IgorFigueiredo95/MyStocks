using MediatR;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Shares.Commands;

public record UpdateShareCommand(
    string Code,
    string? Name,
    string? Description,
    string? shareTypeCode) : IRequest<Result>;
