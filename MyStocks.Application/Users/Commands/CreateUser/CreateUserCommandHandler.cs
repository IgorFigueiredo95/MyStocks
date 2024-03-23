using MediatR;
using MyStocks.Domain.Common;
using MyStocks.Domain.Common.ResultObject;
using MyStocks.Domain.Users;
using MyStocks.Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Application.Users.Commands;

public class CreateUserCommandhandler : IRequestHandler<CreateUserCommand, Result<User>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateUserCommandhandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Email.TryCreate(request.Email, out var userEmail);

        if(userEmail is null) 
            return Error.Create("USER_EMAIL_INVALID", $"The email '{request.Email}' is not a valid email.");

        var isUniqueUserName = await _userRepository.EmailIsUniqueAsync(userEmail);

        if(!isUniqueUserName)
            return Error.Create("USER_EMAIL_CONFLICT", $"The email '{request.Email}' is already in use.");

        User user;
        try
        {
             user = User.Create(request.FirstName, request.LastName, userEmail, request.Password);
            _userRepository.CreateUser(user);
            await _unitOfWork.DispatchDomainEventsAsync(user.RaisedEvents);
            await _unitOfWork.CommitAsync();

        }
        catch (Exception ex)
        {

            return Error.Create("INVALID_OPERATION", ex.Message);
        }

        return user;
    }
}
