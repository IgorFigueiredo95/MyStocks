using MyStocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.PortfolioAggregate.Exceptions;

public class InvalidPortifolioDescriptionException : InvalidLengthException
{
    public InvalidPortifolioDescriptionException(string fieldName) : base(fieldName) { }
    public InvalidPortifolioDescriptionException(string message, Exception innerException) : base(message, innerException) { }
    public InvalidPortifolioDescriptionException(string fieldName, int maxLength) : base(fieldName, maxLength) { }
    public InvalidPortifolioDescriptionException(string fieldName, int maxLength, int minLength):base(fieldName, minLength, maxLength) { }
}
