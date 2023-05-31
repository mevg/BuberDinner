using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Interfaces;
using BuberDinner.Application.Common.Persistance;
using BuberDinner.Application.Common.Services;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.User;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IDateTimeProvider dateTimeProvider)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        //check if user already exists
        if (_userRepository.GetUserByEmail(request.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }
        var currentDate = _dateTimeProvider.Now;
        //create user
        var user = User.CreateUser(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password,
            currentDate,
            currentDate);

        _userRepository.Add(user);
        //create jwt token
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token
        );
    }
}