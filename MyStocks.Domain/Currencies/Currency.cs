using MyStocks.Domain.Exceptions;
using MyStocks.Domain.Primitives;

namespace MyStocks.Domain.Currencies
{
    public class Currency : ValueObject
    {
        public CurrencyTypes CurrencyType { get; private set; }
        public decimal Value { get; set; }

        public override List<object> GetAtomicValues()
        {
            List<object> values = new List<object>
            {
                CurrencyType,
                Value
            };

            return values;
        }

        //Compatibility with EFCore
        public Currency()
        {
            
        }
        private Currency(CurrencyTypes currencyType, decimal value)
        {
            CurrencyType = currencyType;
            Value = value;
        }

        public static Currency Create(CurrencyTypes currencyType, decimal value)
        {
            if (currencyType is null)
                throw new InvalidValueException(nameof(currencyType),new ArgumentNullException(nameof(currencyType)));
            if (value < 0)
                throw new InvalidValueException(nameof(value),new InvalidOperationException(nameof(value)));

            return new Currency(currencyType, value);

        }

        public override string ToString()
        {
            return $"{CurrencyType.Code} {Value}";
        }
    }
}
