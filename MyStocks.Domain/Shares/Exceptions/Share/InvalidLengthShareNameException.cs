using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Shares.Exceptions.Shares;

public class InvalidLengthShareNameException : InvalidLengthException
{
        public InvalidLengthShareNameException(string fieldName) : base(fieldName) { }

        public InvalidLengthShareNameException(string message, Exception? innerException) : base(message, innerException) { }

        public InvalidLengthShareNameException(string fieldName, int minlength, int maxlength)
        : base(fieldName, minlength, maxlength) { }

        public InvalidLengthShareNameException(string fieldName, int maxlength)
        : base(fieldName, maxlength) { }

}
