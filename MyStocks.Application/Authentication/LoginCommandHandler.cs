using MediatR;
using MyStocks.Application.Abstractions;
using MyStocks.Domain.Common;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Users;
using MyStocks.Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Authentication;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJWTProvider _jwtProvider;

    public LoginCommandHandler(IUserRepository userRepository, IJWTProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var userEmail = Email.Create(request.Email);
        var user = await _userRepository.GetUserByEmailAsync(userEmail);

        //todo: fazer corretamente a verificação de senha da request e usuário.
        if (user is null || user.Password != request.password)
            return Error.Create("EMAIL_PASSWORD_INVALID", "username/password are incorrect.");

        return _jwtProvider.GenerateToken(user);
    }
}
