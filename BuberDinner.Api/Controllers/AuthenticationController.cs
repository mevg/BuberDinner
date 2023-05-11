using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;


[Route("api/[controller]")]
public class AuthenticationController : BaseApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = _authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);
        return authResult.Match(
            result =>
            {
                return Ok(MapAuthResult(result));
            },
            errors => Problem(errors)
        );
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult result) =>
        new AuthenticationResponse(
                        result.User.Id,
                        result.User.FirstName,
                        result.User.LastName,
                        result.User.Email,
                        result.Token);

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(
            request.Email,
            request.Password);
        return authResult.Match(
            result =>
            {
                return Ok(MapAuthResult(result));
            },
            errors => Problem(errors)
        );
    }
}