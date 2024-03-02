using MediatR;
using MyStocks.Domain.Common.ResultObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Portfolios.Commands.RemoveShareFromPortfolio;

public record RemoveShareFromPortfolioCommand(Guid PortfolioId, string ShareCode):IRequest<Result>;
