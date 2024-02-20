using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Shares.Exceptions.Shares;

public class NameCannotBeEmptyShareException : InvalidValueException
{
        public NameCannotBeEmptyShareException(string fieldName) : base(fieldName) { }

        public NameCannotBeEmptyShareException(string message, Exception? innerException) : base(message, innerException) { }
}
