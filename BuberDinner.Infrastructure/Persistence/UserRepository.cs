using BuberDinner.Application.Common.Persistance;
using BuberDinner.Domain.User;

namespace BuberDinner.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = new();
    public void Add(User user)
    {
        _users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return _users.SingleOrDefault(u => u.Email == email);
    }
}
