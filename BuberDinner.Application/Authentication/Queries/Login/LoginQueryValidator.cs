using FluentValidation;

namespace BuberDinner.Application.Authentication.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(p => p.Email).NotEmpty()
            .EmailAddress();
        RuleFor(p => p.Password)
            .NotEmpty()
            .MinimumLength(4);
    }
}