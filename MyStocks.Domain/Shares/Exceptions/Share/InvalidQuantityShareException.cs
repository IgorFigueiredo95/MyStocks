using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Shares.Exceptions.Shares;

public class InvalidQuantityShareException : InvalidLengthException
{
    public InvalidQuantityShareException(string fieldName) : base(fieldName) { }

    public InvalidQuantityShareException(string message, Exception? innerException) : base(message, innerException) { }

    public InvalidQuantityShareException(string fieldName, int minlength, int maxlength)
    : base(fieldName, minlength, maxlength) { }

    public InvalidQuantityShareException(string fieldName, int maxlength)
    : base(fieldName, maxlength) { }

}
