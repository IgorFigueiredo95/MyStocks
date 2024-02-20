using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Exceptions;

public class InvalidValueException:Exception
{
    public InvalidValueException(string fieldName, Exception innerException)
    : base($"'{fieldName}' is invalid. {innerException?.Message}", innerException) { }
    
    public InvalidValueException(string fieldName) : base($"'{fieldName}' is invalid.") { }

    public InvalidValueException() : base() { }

}
