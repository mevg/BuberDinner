using BuberDinner.Domain.User;

namespace BuberDinner.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
