using MyStocks.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Users.ValueObjects;

public class Email : ValueObject
{
    public string Address { get; private set; }

    public bool IsValid { get; private set; }

    private Email(string address, bool? isValidated)
    {
        Address = address;
        IsValid = isValidated ?? false;
    }

    public static Email Create(string address)
    {
        if (!TryCreate(address, out var email))
            throw new ArgumentException($"The string '{address}' is an invalid email.");

        return email;
    }

    public static bool TryCreate(string address, out Email? Email)
    {
       var canCreate = MailAddress.TryCreate(address, out var mailAddress);

        if (canCreate)
        {
            Email = new Email(address,true);
            return true;
        }

        Email = null;
        return false;
    }

    public override List<object> GetAtomicValues()
    {
        return new List<object>() { Address };
    }
}
