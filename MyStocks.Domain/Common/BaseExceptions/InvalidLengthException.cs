using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Exceptions;

public class InvalidLengthException : Exception
{
    public InvalidLengthException(string fieldName) : base($"'{fieldName}' has invalid length.") { }

    public InvalidLengthException(string message, Exception? innerException) : base(message, innerException) {}

    public InvalidLengthException(string fieldName, int minlength, int maxlength) 
        : base($"'{fieldName}' has invalid length. '{fieldName}' must be between {minlength} and {maxlength} characters.") { }

    public InvalidLengthException(string fieldName, int maxlength)
        : base($"'{fieldName}' has invalid length. '{fieldName}' cannot be bigger than {maxlength} characters.") { }
}
