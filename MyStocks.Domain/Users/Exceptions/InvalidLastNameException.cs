using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Users.Exceptions;

internal class InvalidLastNameException : InvalidLengthException
{
    public InvalidLastNameException(string fieldName) : base(fieldName)
    {
    }

    public InvalidLastNameException(string message, Exception? innerException) : base(message, innerException)
    {
    }

    public InvalidLastNameException(string fieldName, int maxlength) : base(fieldName, maxlength)
    {
    }

    public InvalidLastNameException(string fieldName, int minlength, int maxlength) : base(fieldName, minlength, maxlength)
    {
    }
}
