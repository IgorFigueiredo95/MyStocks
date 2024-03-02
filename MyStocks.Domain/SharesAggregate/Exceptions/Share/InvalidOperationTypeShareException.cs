using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Shares.Exceptions.Shares;

public class InvalidOperationTypeShareException : InvalidLengthException
{
    public InvalidOperationTypeShareException(string fieldName) : base(fieldName) { }

    public InvalidOperationTypeShareException(string message, Exception? innerException) : base(message, innerException) { }

    public InvalidOperationTypeShareException(string fieldName, int minlength, int maxlength)
    : base(fieldName, minlength, maxlength) { }

    public InvalidOperationTypeShareException(string fieldName, int maxlength)
    : base(fieldName, maxlength) { }

}
