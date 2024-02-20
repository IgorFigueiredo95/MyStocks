using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Exceptions;

public class ValueNotFoundException : Exception
{
    public ValueNotFoundException(string fieldName)
        : base($"'{fieldName}' not found.") { }

    public ValueNotFoundException(string fieldName, Exception? innerException) : base($"'{fieldName}' not found. {innerException.Message}", innerException) { }

    public ValueNotFoundException() : base() { }
}