using MediatR;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Users.Commands;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password):IRequest<Result<User>>;
