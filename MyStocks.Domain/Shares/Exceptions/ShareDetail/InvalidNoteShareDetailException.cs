using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Shares.Exceptions.SharesDetail;

public class InvalidNoteShareDetailException : InvalidLengthException
{
        public InvalidNoteShareDetailException(string fieldName) : base(fieldName) { }

        public InvalidNoteShareDetailException(string message, Exception? innerException) : base(message, innerException) { }

        public InvalidNoteShareDetailException(string fieldName, int minlength, int maxlength)
        : base(fieldName, minlength, maxlength) { }

        public InvalidNoteShareDetailException(string fieldName, int maxlength)
        : base(fieldName, maxlength) { }

}
