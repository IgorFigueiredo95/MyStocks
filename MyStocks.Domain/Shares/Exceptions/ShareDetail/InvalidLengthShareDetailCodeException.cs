using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Shares.Exceptions.SharesDetail;

public class InvalidLengthShareDetailCodeException : InvalidLengthException
{
        public InvalidLengthShareDetailCodeException(string fieldName) : base(fieldName) { }

        public InvalidLengthShareDetailCodeException(string message, Exception? innerException) : base(message, innerException) { }

        public InvalidLengthShareDetailCodeException(string fieldName, int minlength, int maxlength)
        : base(fieldName, minlength, maxlength) { }

        public InvalidLengthShareDetailCodeException(string fieldName, int maxlength)
        : base(fieldName, maxlength) { }

}
