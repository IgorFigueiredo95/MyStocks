using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Shares.Exceptions.SharesDetail;

public class InvalidQuantityShareDetailException : InvalidLengthException
{
        public InvalidQuantityShareDetailException(string fieldName) : base(fieldName) { }

        public InvalidQuantityShareDetailException(string message, Exception? innerException) : base(message, innerException) { }

        public InvalidQuantityShareDetailException(string fieldName, int minlength, int maxlength)
        : base(fieldName, minlength, maxlength) { }

        public InvalidQuantityShareDetailException(string fieldName, int maxlength)
        : base(fieldName, maxlength) { }

}
