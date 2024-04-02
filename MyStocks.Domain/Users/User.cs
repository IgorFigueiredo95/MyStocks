using MyStocks.Domain.Common.Primitives;
using MyStocks.Domain.Primitives;
using MyStocks.Domain.Users.Exceptions;
using MyStocks.Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Users;

public class User: Entity , IAggregateRoot
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }
    public string Password { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private User(string firstName, string lastName, Email email, string password) : base (Guid.NewGuid())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        CreatedAt = DateTime.Now;
    }

    public static User Create(string firstName, string lastName, Email email, string password)
    {
        if (firstName.Length <= 3 || firstName.Length > UserConstants.MAX_USERNAME_LENGTH)
            throw new InvalidFirstNameException(nameof(firstName));

        if (lastName.Length <= 3 || lastName.Length > UserConstants.MAX_USERLASTNAME_LENGTH)
            throw new InvalidLastNameException(nameof(lastName));

        //todo: validação para senha. talvez gerar um value object somente para ela.
       return new  User(firstName, lastName, email, password);
    }
}
