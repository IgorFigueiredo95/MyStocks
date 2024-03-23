using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Contracts.Users;

public record CreateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);
