using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Contracts.Shares;
public record CreateShareDetailRequest(
    string ShareCode,
    string? Note,
    decimal Quantity,
    decimal Price,
    string OperationTypeCode);
