using MyStocks.Domain.Exceptions;
using MyStocks.Domain.Primitives;
namespace MyStocks.Domain.Currencies;

public class CurrencyTypes : Entity
{
    public string Code { get; private set; }
    public string CurrencyCode { get; private set; }
    public string Name { get; private set; }
    public bool IsDefault { get; private set; } = false;


    private CurrencyTypes(Guid id, string code, string currencyCode, string name)
        : base(id)
    {
        Code = code;
        CurrencyCode = currencyCode;
        Name = name;
    }

    public static CurrencyTypes Create(string Code, string currencyCode, string name)
    {

        if (Code.Length > Constants.MAX_CODE_LENGTH)
            throw new InvalidLengthException(nameof(Code),Constants.MAX_CODE_LENGTH);

        if (currencyCode.Length > Constants.MAX_CURRENCYCODE_LENGTH)
            throw new InvalidLengthException(nameof(currencyCode), Constants.MAX_CURRENCYCODE_LENGTH);

        if (name.Length > Constants.MAX_CURRENCYNAME_LENGTH)
            throw new InvalidLengthException(nameof(name), Constants.MAX_CURRENCYNAME_LENGTH);

        return new CurrencyTypes(Guid.NewGuid(), Code.Trim(), currencyCode.Trim().ToUpper(), name.Trim());
    }

    public void UpdateName(string name)
    {
        Name = name.Trim();

        if (name.Length > Constants.MAX_CURRENCYNAME_LENGTH)
            throw new InvalidLengthException(nameof(name), Constants.MAX_CURRENCYNAME_LENGTH);
    }

    public void SetValueDefaultCurrency(bool value)
    {
        IsDefault = value;
    }
}
