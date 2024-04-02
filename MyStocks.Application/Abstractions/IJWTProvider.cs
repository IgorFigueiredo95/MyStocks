using MyStocks.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Abstractions;

public interface IJWTProvider
{
    public string GenerateToken(User user);
}
