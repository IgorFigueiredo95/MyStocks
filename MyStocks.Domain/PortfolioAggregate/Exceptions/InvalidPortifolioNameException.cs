using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.PortfolioAggregate.Exceptions;

public class InvalidPortifolioNameLengthException : InvalidLengthException
{
    public InvalidPortifolioNameLengthException(string fieldName) : base(fieldName) { }
    public InvalidPortifolioNameLengthException(string message, Exception innerException) : base(message, innerException) { }
    public InvalidPortifolioNameLengthException(string fieldName, int maxLength) : base(fieldName, maxLength) { }
    public InvalidPortifolioNameLengthException(string fieldName, int maxLength, int minLength):base(fieldName, minLength, maxLength) { }
}
