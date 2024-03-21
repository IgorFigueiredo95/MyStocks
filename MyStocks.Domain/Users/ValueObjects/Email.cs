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

    private Email(string address)
    {
        Address = address;
    }

    public static Email Create(string address)
    {
        if (Validate(address))
            throw new ArgumentException($"The string '{address}' is an invalid email.");

        Email email = new Email(address);
        email.IsValid = true;

        return email;
    }

    public static bool Validate(string address)
    {
       return  MailAddress.TryCreate(address, out var mailAddress);
    }

    public override List<object> GetAtomicValues()
    {
        return new List<object>() { Address };
    }
}
