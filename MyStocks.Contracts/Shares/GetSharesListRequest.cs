using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Contracts.Shares;

public record GetShareListRequest(int? Limit, int? Offset );
