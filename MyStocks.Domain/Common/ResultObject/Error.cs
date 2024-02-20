using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Common;

public sealed class Error
{
    public string Key { get; private set; }
    public string Message { get; private set; }

    private Error(string key, string message)
    {
        Key = key;
        Message = message;
    }

    public static Error Create(string key, string message)
    {
        return new Error(key, message);
    }
}
