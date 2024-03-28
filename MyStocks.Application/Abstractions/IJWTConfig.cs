using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Abstractions;

public interface IJWTConfig
{
    public string Key { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public int ExpiresInHours { get; set; }
}
