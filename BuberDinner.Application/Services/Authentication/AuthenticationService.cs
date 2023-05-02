using BuberDinner.Application.Common.Interfaces;
using BuberDinner.Application.Common.Persistance;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Login(string email, string password)
    {
        //validate the user exists
        if(_userRepository.GetUserByEmail(email) is not User user){
            throw new Exception("User with given email does not exists");
        }
        //validate the password is correct
        if(user.Password != password){
            throw new Exception("Invalid Password");
        }
        //create jwt token
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token
        );
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        //check if user already exists
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            throw new Exception("User with given email already exists");
        }
        //create user 
        var user = new User{ FirstName = firstName, LastName = lastName, Email = email, Password = password };
        _userRepository.Add(user);
        //create jwt token
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token
        );
    }
}
