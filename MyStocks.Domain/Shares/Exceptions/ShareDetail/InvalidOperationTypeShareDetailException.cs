using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Shares.Exceptions.SharesDetail;

public class InvalidOperationTypeShareDetailException : InvalidLengthException
{
        public InvalidOperationTypeShareDetailException(string fieldName) : base(fieldName) { }

        public InvalidOperationTypeShareDetailException(string message, Exception? innerException) : base(message, innerException) { }

        public InvalidOperationTypeShareDetailException(string fieldName, int minlength, int maxlength)
        : base(fieldName, minlength, maxlength) { }

        public InvalidOperationTypeShareDetailException(string fieldName, int maxlength)
        : base(fieldName, maxlength) { }

}
