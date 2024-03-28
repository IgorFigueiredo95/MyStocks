using Microsoft.Extensions.Configuration;
using MyStocks.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Infrastructure.Authentication;

public class JWTConfig: IJWTConfig
{
    public string Key { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public int ExpiresInHours { get; set; }
}
