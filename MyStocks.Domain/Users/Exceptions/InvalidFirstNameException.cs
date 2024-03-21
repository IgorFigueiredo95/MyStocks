using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Users.Exceptions;

internal class InvalidFirstNameException : InvalidLengthException
{
    public InvalidFirstNameException(string fieldName) : base(fieldName)
    {
    }

    public InvalidFirstNameException(string message, Exception? innerException) : base(message, innerException)
    {
    }

    public InvalidFirstNameException(string fieldName, int maxlength) : base(fieldName, maxlength)
    {
    }

    public InvalidFirstNameException(string fieldName, int minlength, int maxlength) : base(fieldName, minlength, maxlength)
    {
    }
}
