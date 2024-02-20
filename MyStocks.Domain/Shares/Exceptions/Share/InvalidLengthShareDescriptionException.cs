using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Shares.Exceptions.Shares;

public class InvalidLengthShareDescriptionException : InvalidLengthException
{
        public InvalidLengthShareDescriptionException(string fieldName) : base(fieldName) { }

        public InvalidLengthShareDescriptionException(string message, Exception? innerException) : base(message, innerException) { }

        public InvalidLengthShareDescriptionException(string fieldName, int minlength, int maxlength)
        : base(fieldName, minlength, maxlength) { }

        public InvalidLengthShareDescriptionException(string fieldName, int maxlength)
        : base(fieldName, maxlength) { }

}
