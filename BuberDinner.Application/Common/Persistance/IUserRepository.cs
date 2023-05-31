using BuberDinner.Domain.User;

namespace BuberDinner.Application.Common.Persistance;
public interface IUserRepository
{
    void Add(User user);
    User? GetUserByEmail(string email);
}