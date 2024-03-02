﻿using MediatR;
using MyStocks.Domain.Common.ResultObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Portfolios.Commands;

public record CreatePortfolioCommand(string Name, string? Description):IRequest<Result<Guid>>;