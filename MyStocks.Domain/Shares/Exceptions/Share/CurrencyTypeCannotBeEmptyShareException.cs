using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Shares.Exceptions.Shares;

public class CurrencyTypeCannotBeEmptyShareException : InvalidValueException
{
        public CurrencyTypeCannotBeEmptyShareException(string fieldName) : base(fieldName) { }

        public CurrencyTypeCannotBeEmptyShareException(string message, Exception? innerException) : base(message, innerException) { }

}
