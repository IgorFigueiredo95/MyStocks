using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Shares.Exceptions.Shares;

public class InvalidCurrencyTypeShareException : InvalidLengthException
{
        public InvalidCurrencyTypeShareException(string fieldName) : base(fieldName) { }

        public InvalidCurrencyTypeShareException(string message, Exception? innerException) : base(message, innerException) { }

        public InvalidCurrencyTypeShareException(string fieldName, int minlength, int maxlength)
        : base(fieldName, minlength, maxlength) { }

        public InvalidCurrencyTypeShareException(string fieldName, int maxlength)
        : base(fieldName, maxlength) { }

}
