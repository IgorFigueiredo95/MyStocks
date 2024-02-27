using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Contracts;

public record CreatePortfolioRequest(string Name,string? Description);
