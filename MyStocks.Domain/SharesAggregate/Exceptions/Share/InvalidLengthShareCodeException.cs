using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Shares.Exceptions.Shares;

public class InvalidLengthShareCodeException : InvalidLengthException
{
        public InvalidLengthShareCodeException(string fieldName) : base(fieldName) { }

        public InvalidLengthShareCodeException(string message, Exception? innerException) : base(message, innerException) { }

        public InvalidLengthShareCodeException(string fieldName, int minlength, int maxlength)
        : base(fieldName, minlength, maxlength) { }

        public InvalidLengthShareCodeException(string fieldName, int maxlength)
        : base(fieldName, maxlength) { }

}
